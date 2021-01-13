package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
	"sync"
)

type operation string

const (
	jmp operation = "jmp"
	acc operation = "acc"
	nop operation = "nop"
)

type instruction struct {
	op  operation
	arg int
}

type output struct {
	acc   int
	halts bool
}

func parseInput() []instruction {
	file, _ := os.Open("../inputs/D8.input")
	defer file.Close()

	program := make([]instruction, 0)
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		fields := strings.Fields(line)
		op := operation(fields[0])
		arg, _ := strconv.Atoi(fields[1])
		instruction := instruction{
			op:  op,
			arg: arg,
		}

		program = append(program, instruction)
	}

	return program
}

func run(program []instruction, channel *chan output, waitGroup *sync.WaitGroup) {
	defer waitGroup.Done()

	seen := map[int]bool{}
	pptr := 0
	accm := 0

	for {
		if seen[pptr] {
			*channel <- output{acc: accm, halts: false}
			return
		}
		if pptr >= len(program) {
			*channel <- output{acc: accm, halts: true}
			return
		}
		seen[pptr] = true
		instruction := program[pptr]
		switch instruction.op {
		case jmp:
			pptr = pptr + instruction.arg
		case acc:
			accm += instruction.arg
			pptr++
		case nop:
			pptr++
		}
	}
}

func p1() int {
	program := parseInput()
	channel := make(chan output, 1)
	var waitGroup sync.WaitGroup

	waitGroup.Add(1)

	go run(program, &channel, &waitGroup)

	waitGroup.Wait()

	return (<-channel).acc
}

func p2() int {
	program := parseInput()
	channel := make(chan output, len(program))
	var waitGroup sync.WaitGroup

	for i := range program {
		modified := parseInput()
		switch modified[i].op {
		case acc:
			continue
		case jmp:
			modified[i].op = nop
		case nop:
			modified[i].op = jmp
		}
		waitGroup.Add(1)
		go run(modified, &channel, &waitGroup)
	}

	waitGroup.Wait()

	for i := 0; i < len(program); i++ {
		out := <-channel
		if out.halts {
			return out.acc
		}
	}
	return -1
}

func main() {
	fmt.Println(p1())
	fmt.Println(p2())
}
