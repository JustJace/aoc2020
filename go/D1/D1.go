package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func parseInput() []int {
	file, _ := os.Open("../inputs/D1.input")
	defer file.Close()

	scanner := bufio.NewScanner(file)

	nums := make([]int, 0, 100)

	for scanner.Scan() {
		num, _ := strconv.Atoi(scanner.Text())
		nums = append(nums, num)
	}

	return nums
}

func checkForSum(n int, nums []int, c chan int) {
	for _, num := range nums {
		if n+num == 2020 {
			c <- n * num
			close(c)
		}
	}
}

func p1() int {
	nums := parseInput()
	c := make(chan int, 1)

	for i, num := range nums {
		go checkForSum(num, nums[i+1:], c)
	}

	return <-c
}

func checkForSum2(n int, nums []int, c chan int) {
	for i, num1 := range nums {
		for _, num2 := range nums[i+1:] {
			if n+num1+num2 == 2020 {
				c <- n * num1 * num2
				close(c)
			}
		}
	}
}

func p2() int {
	nums := parseInput()
	c := make(chan int, 1)

	for i, num := range nums {
		go checkForSum2(num, nums[i+1:], c)
	}

	return <-c
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
