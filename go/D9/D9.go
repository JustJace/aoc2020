package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func parseInput() []int {
	file, _ := os.Open("../inputs/D9.input")
	defer file.Close()

	nums := make([]int, 0)
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		num, _ := strconv.Atoi(line)
		nums = append(nums, num)
	}

	return nums
}

func findWeakness(nums []int) int {

	for i := 25; i < len(nums); i++ {
		found := false
		for j, a := range nums[i-25 : i] {
			for _, b := range nums[j+1 : i] {
				if a+b == nums[i] {
					found = true
					break
				}
			}
			if found {
				break
			}
		}

		if !found {
			return nums[i]
		}
	}

	return -1
}

func p1() int {
	nums := parseInput()
	return findWeakness(nums)
}

func sum(nums []int) int {
	sum := 0
	for _, num := range nums {
		sum += num
	}
	return sum
}

func min(nums []int) int {
	min := 1 << 32
	for _, num := range nums {
		if num < min {
			min = num
		}
	}
	return min
}

func max(nums []int) int {
	max := -1 << 32
	for _, num := range nums {
		if num > max {
			max = num
		}
	}
	return max
}

func p2() int {
	nums := parseInput()
	weakness := findWeakness(nums)

	sum := sum(nums[0:2])
	L := 0
	R := 1

	for sum != weakness {
		if sum > weakness {
			sum -= nums[L]
			L++
		} else {
			R++
			sum += nums[R]
		}
	}

	return min(nums[L:R+1]) + max(nums[L:R+1])
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
