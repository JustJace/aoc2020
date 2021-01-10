package main

import (
	"bufio"
	"fmt"
	"os"
	"sync"
)

func parseInput() [][]rune {
	file, _ := os.Open("../inputs/D3.input")
	defer file.Close()

	grid := make([][]rune, 0, 100)

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		runes := []rune(line)
		grid = append(grid, runes)
	}

	return grid
}

func treeSlope(grid *[][]rune, channel *chan int, wg *sync.WaitGroup, dr int, dc int) {
	defer wg.Done()

	r := 0
	c := 0
	w := len((*grid)[0])
	count := 0

	for r < len(*grid) {

		if (*grid)[r][c%w] == '#' {
			count++
		}

		r += dr
		c += dc
	}

	*channel <- count
}

func p1() int {
	grid := parseInput()
	c := make(chan int, 1)
	var wg sync.WaitGroup

	wg.Add(1)

	go treeSlope(&grid, &c, &wg, 1, 3)

	wg.Wait()

	return <-c
}

func p2() int {
	grid := parseInput()
	c := make(chan int, 5)
	var wg sync.WaitGroup

	wg.Add(5)

	go treeSlope(&grid, &c, &wg, 1, 1)
	go treeSlope(&grid, &c, &wg, 1, 3)
	go treeSlope(&grid, &c, &wg, 1, 5)
	go treeSlope(&grid, &c, &wg, 1, 7)
	go treeSlope(&grid, &c, &wg, 2, 1)

	wg.Wait()

	product := 1

	for i := 0; i < 5; i++ {
		product *= <-c
	}

	return product
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
