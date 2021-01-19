from dataclasses import dataclass
import copy

class op:
    JMP = "jmp"
    NOP = "nop"
    ACC = "acc"

@dataclass
class instruction:
    op: op
    arg: int

def parseInput():
    file = open("../inputs/D8.input", "r")
    program = []
    contents = file.read()
    for line in contents.splitlines():
        split = line.split()
        program.append(instruction(split[0], int(split[1])))
    return program

def run(program):
    seen = set()
    pptr = 0
    acc = 0
    while True:
        if pptr in seen: return acc, False
        if pptr >= len(program): return acc, True
        seen.add(pptr)
        
        instruction = program[pptr]

        if instruction.op == op.JMP:
            pptr += instruction.arg
        elif instruction.op == op.ACC:
            acc += instruction.arg
            pptr += 1
        elif instruction.op == op.NOP:
            pptr += 1

def p1():
    acc, halts = run(parseInput())
    return acc

def p2():
    program = parseInput()
    for i in range(len(program)):
        modified = parseInput()
        instruction = modified[i]
        
        if instruction.op == op.JMP:
            instruction.op = op.NOP
        elif instruction.op == op.ACC:
            continue
        elif instruction.op == op.NOP:
            instruction.op = op.JMP

        acc, halts = run(modified)

        if halts: return acc

print(p1())
print(p2())
