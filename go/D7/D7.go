package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type bag struct {
	name     string
	parents  []string
	children []baginfo
}

type baginfo struct {
	count int
	name  string
}

func parseInput() map[string]*bag {
	file, _ := os.Open("../inputs/D7.input")
	defer file.Close()

	bags := make(map[string]*bag, 0)
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		containsplit := strings.Split(line, " contain ")
		name := containsplit[0]
		name = name[:len(name)-5]
		if _, ok := bags[name]; !ok {
			bags[name] = &bag{name: name}
		}
		if containsplit[1] == "no other bags." {
			continue
		}
		parentBag := bags[name]
		for _, info := range strings.Split(containsplit[1], ", ") {
			spacesplit := strings.Split(info, " ")
			count, _ := strconv.Atoi(spacesplit[0])
			child := strings.ReplaceAll(strings.Join(spacesplit[1:], " "), ".", "")
			if count == 1 {
				child = child[:len(child)-4]
			} else {
				child = child[:len(child)-5]
			}
			parentBag.children = append(parentBag.children, baginfo{
				name:  child,
				count: count,
			})
			if _, ok := bags[child]; !ok {
				bags[child] = &bag{name: child}
			}
			childBag := bags[child]
			childBag.parents = append(childBag.parents, name)
		}
	}

	return bags
}

func countAncestry(bags *map[string]*bag, bag *bag) int {
	seen := map[string]bool{}
	q := []string{bag.name}

	for len(q) > 0 {
		current := q[0]
		q = q[1:]
		if _, ok := seen[current]; ok {
			continue
		}

		seen[current] = true

		for _, parent := range (*bags)[current].parents {
			q = append(q, parent)
		}
	}

	return len(seen) - 1
}

func p1() int {
	bags := parseInput()
	shinygold := bags["shiny gold"]
	return countAncestry(&bags, shinygold)
}

func countLineage(bags *map[string]*bag, bag *bag) int {
	count := 0

	for _, child := range bag.children {
		count += child.count * (1 + countLineage(bags, (*bags)[child.name]))
	}

	return count
}

func p2() int {
	bags := parseInput()
	shinygold := bags["shiny gold"]
	return countLineage(&bags, shinygold)
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
