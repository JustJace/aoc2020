package main

import (
	"bufio"
	"fmt"
	"os"
	"sync"
)

func parseInput() [][]string {
	file, _ := os.Open("../inputs/D6.input")
	defer file.Close()

	groups := make([][]string, 0)
	scanner := bufio.NewScanner(file)

	current := make([]string, 0)

	for scanner.Scan() {
		line := scanner.Text()
		if len(line) == 0 {
			groups = append(groups, current)
			current = make([]string, 0)
		} else {
			current = append(current, line)
		}
	}

	return append(groups, current)
}

func countDistinctQuestions(group []string, channel *chan int, waitGroup *sync.WaitGroup) {
	defer waitGroup.Done()

	questions := map[rune]bool{}

	for _, person := range group {
		for _, question := range person {
			questions[question] = true
		}
	}

	*channel <- len(questions)
}

func p1() int {
	groups := parseInput()
	channel := make(chan int, len(groups))
	var waitGroup sync.WaitGroup

	for _, group := range groups {
		waitGroup.Add(1)
		go countDistinctQuestions(group, &channel, &waitGroup)
	}

	waitGroup.Wait()

	count := 0
	for i := 0; i < len(groups); i++ {
		count += <-channel
	}

	return count
}

func countUnanimousQuestions(group []string, channel *chan int, waitGroup *sync.WaitGroup) {
	defer waitGroup.Done()

	questions := make(map[rune]int)

	for _, person := range group {
		for _, question := range person {
			_, ok := questions[question]
			if !ok {
				questions[question] = 0
			}
			questions[question]++
		}
	}

	count := 0
	for _, answerCount := range questions {
		if answerCount == len(group) {
			count++
		}
	}

	*channel <- count
}

func p2() int {
	groups := parseInput()
	channel := make(chan int, len(groups))
	var waitGroup sync.WaitGroup

	for _, group := range groups {
		waitGroup.Add(1)
		go countUnanimousQuestions(group, &channel, &waitGroup)
	}

	waitGroup.Wait()

	count := 0
	for i := 0; i < len(groups); i++ {
		count += <-channel
	}

	return count
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
