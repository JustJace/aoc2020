package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"
	"sync"
)

type rule struct {
	lo int
	hi int
	ch string
	pw string
}

func parseInput() []rule {
	file, _ := os.Open("../inputs/D2.input")
	defer file.Close()

	scanner := bufio.NewScanner(file)

	rules := make([]rule, 0, 100)
	regex := regexp.MustCompile("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)")

	for scanner.Scan() {
		match := regex.FindAllStringSubmatch(scanner.Text(), -1)
		lo, _ := strconv.Atoi(match[0][1])
		hi, _ := strconv.Atoi(match[0][2])
		rules = append(rules, rule{
			lo: lo,
			hi: hi,
			ch: match[0][3],
			pw: match[0][4]})
	}

	return rules
}

func validateRule(rule rule, c *chan bool, wg *sync.WaitGroup) {
	defer wg.Done()

	count := 0

	for _, ch := range rule.pw {
		if string(ch) == rule.ch {
			count++
			if count > rule.hi {
				*c <- false
				return
			}
		}
	}

	*c <- (count >= rule.lo) && (count <= rule.hi)
}

func p1() int {
	rules := parseInput()
	c := make(chan bool, len(rules))
	var wg sync.WaitGroup

	for _, rule := range rules {
		wg.Add(1)
		go validateRule(rule, &c, &wg)
	}

	wg.Wait()

	count := 0

	for i := 0; i < len(rules); i++ {
		valid := <-c
		if valid {
			count++
		}
	}

	return count
}

func validateRule2(rule rule, c *chan bool, wg *sync.WaitGroup) {
	defer wg.Done()

	lo := string(rule.pw[rule.lo-1]) == rule.ch
	hi := string(rule.pw[rule.hi-1]) == rule.ch

	*c <- lo != hi
}

func p2() int {
	rules := parseInput()
	c := make(chan bool, len(rules))
	var wg sync.WaitGroup

	for _, rule := range rules {
		wg.Add(1)
		go validateRule2(rule, &c, &wg)
	}

	wg.Wait()

	count := 0

	for i := 0; i < len(rules); i++ {
		valid := <-c
		if valid {
			count++
		}
	}

	return count
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
