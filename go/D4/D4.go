package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
	"sync"
)

func parseInput() []map[string]string {
	file, _ := os.Open("../inputs/D4.input")
	defer file.Close()

	passports := make([]map[string]string, 0)
	scanner := bufio.NewScanner(file)

	current := make(map[string]string)

	for scanner.Scan() {
		line := scanner.Text()
		if len(line) == 0 {
			passports = append(passports, current)
			current = make(map[string]string)
		}

		for _, field := range strings.Fields(line) {
			keyValue := strings.Split(field, ":")
			current[keyValue[0]] = keyValue[1]
		}
	}

	return append(passports, current)
}

func validateKeys(passport map[string]string) bool {
	if len(passport) == 8 {
		return true
	} else if len(passport) == 7 {
		_, ok := passport["cid"]
		return !ok
	} else {
		return false
	}
}

func validatePassportKeys(passport map[string]string, channel *chan bool, waitGroup *sync.WaitGroup) {
	defer waitGroup.Done()
	*channel <- validateKeys(passport)
}

func p1() int {
	passports := parseInput()
	channel := make(chan bool, len(passports))
	var waitGroup sync.WaitGroup

	for _, passport := range passports {
		waitGroup.Add(1)
		go validatePassportKeys(passport, &channel, &waitGroup)
	}

	waitGroup.Wait()

	valid := 0
	for i := 0; i < len(passports); i++ {
		if <-channel {
			valid++
		}
	}

	return valid
}

var eyecolors = map[string]bool{
	"amb": true,
	"blu": true,
	"brn": true,
	"gry": true,
	"grn": true,
	"hzl": true,
	"oth": true,
}

func validateValues(passport map[string]string) bool {
	for key, value := range passport {
		switch key {
		case "byr":
			byr, _ := strconv.Atoi(value)
			if byr < 1920 || byr > 2002 {
				return false
			}
		case "iyr":
			iyr, _ := strconv.Atoi(value)
			if iyr < 2010 || iyr > 2020 {
				return false
			}
		case "eyr":
			eyr, _ := strconv.Atoi(value)
			if eyr < 2020 || eyr > 2030 {
				return false
			}
		case "hgt":
			nums := value[:len(value)-2]
			if len(nums) <= 0 {
				return false
			}
			hgt, _ := strconv.Atoi(nums)
			unit := value[len(value)-2:]
			if unit == "cm" {
				if hgt < 150 || hgt > 193 {
					return false
				}
			} else if unit == "in" {
				if hgt < 59 || hgt > 76 {
					return false
				}
			} else {
				return false
			}
		case "hcl":
			if len(value) != 7 || value[0] != '#' {
				return false
			}

			hex := value[1:]
			_, err := strconv.ParseInt(hex, 16, 32)
			if err != nil {
				return false
			}
		case "ecl":
			_, ok := eyecolors[value]
			if !ok {
				return false
			}
		case "pid":
			if len(value) != 9 {
				return false
			}
			_, err := strconv.Atoi(value)
			if err != nil {
				return false
			}
		case "cid":
			continue
		}
	}

	return true
}

func validatePassportKeysAndValues(passport map[string]string, channel *chan bool, waitGroup *sync.WaitGroup) {
	defer waitGroup.Done()
	*channel <- validateKeys(passport) && validateValues(passport)
}

func p2() int {
	passports := parseInput()
	channel := make(chan bool, len(passports))
	var waitGroup sync.WaitGroup

	for _, passport := range passports {
		waitGroup.Add(1)
		go validatePassportKeysAndValues(passport, &channel, &waitGroup)
	}

	waitGroup.Wait()

	valid := 0
	for i := 0; i < len(passports); i++ {
		if <-channel {
			valid++
		}
	}

	return valid
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
