package main

import (
	"bufio"
	"fmt"
	"os"
	"sort"
	"strconv"
)

func parseInput() []int {
	file, _ := os.Open("../inputs/D10.input")
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

func max(nums []int) int {
	max := -1 << 32
	for _, num := range nums {
		if num > max {
			max = num
		}
	}
	return max
}

func p1() int {
	nums := parseInput()
	nums = append(nums, 0)
	nums = append(nums, max(nums)+3)
	sort.Ints(nums)
	d1, d3 := 0, 0

	for i := 0; i < len(nums)-1; i++ {
		diff := nums[i+1] - nums[i]
		if diff == 3 {
			d3++
		} else if diff == 1 {
			d1++
		}
	}

	return d1 * d3
}

func p2() uint64 {
	nums := parseInput()
	nums = append(nums, 0)
	nums = append(nums, max(nums)+3)
	sort.Ints(nums)
	dp := make(map[int]uint64, 0)
	size := len(nums)
	dp[size-1] = 0
	dp[size-2] = 1

	for i := size - 3; i >= 0; i-- {
		dp[i] = 0
		if i+1 < size && nums[i+1]-nums[i] <= 3 {
			dp[i] += dp[i+1]
		}
		if i+2 < size && nums[i+2]-nums[i] <= 3 {
			dp[i] += dp[i+2]
		}
		if i+3 < size && nums[i+3]-nums[i] <= 3 {
			dp[i] += dp[i+3]
		}
	}

	return dp[0]
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
