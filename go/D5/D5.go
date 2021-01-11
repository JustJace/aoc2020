package main

import (
	"bufio"
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
	"sync"
)

func parseInput() []string {
	file, _ := os.Open("../inputs/D5.input")
	defer file.Close()

	seats := make([]string, 0, 100)

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		seats = append(seats, scanner.Text())
	}

	return seats
}

func asBinary(s string) int {
	s = strings.ReplaceAll(s, "F", "0")
	s = strings.ReplaceAll(s, "B", "1")
	s = strings.ReplaceAll(s, "L", "0")
	s = strings.ReplaceAll(s, "R", "1")

	num, _ := strconv.ParseInt(s, 2, 32)
	return int(num)
}

func seatID(seat string, channel *chan int, wg *sync.WaitGroup) {
	defer wg.Done()
	row := asBinary(seat[:7])
	col := asBinary(seat[7:])
	seatID := row*8 + col
	*channel <- seatID
}

func p1() int {
	seats := parseInput()
	channel := make(chan int, len(seats))
	var waitGroup sync.WaitGroup

	for _, seat := range seats {
		waitGroup.Add(1)
		go seatID(seat, &channel, &waitGroup)
	}

	waitGroup.Wait()

	max := 0

	for i := 0; i < len(seats); i++ {
		seatID := <-channel
		if seatID > max {
			max = seatID
		}
	}

	return max
}

func p2() int {
	seats := parseInput()
	channel := make(chan int, len(seats))
	var waitGroup sync.WaitGroup

	for _, seat := range seats {
		waitGroup.Add(1)
		go seatID(seat, &channel, &waitGroup)
	}

	waitGroup.Wait()

	seatIDs := make([]int, 0, len(seats))

	for i := 0; i < len(seats); i++ {
		seatIDs = append(seatIDs, <-channel)
	}

	sort.Ints(seatIDs)

	for i, id := range seatIDs {
		if seatIDs[i+1] != id+1 {
			return id + 1
		}
	}

	return -1
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
