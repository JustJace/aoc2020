import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

enum op {
    nop = "nop",
    jmp = "jmp",
    acc = "acc"
}

class instruction {
    op: op;
    arg: number;
}

function parseInput(): instruction[] {
    const instructions = [];
    const content = readFileSync('../inputs/D8.input','utf8');
    for (let line of content.split("\r\n")) {
        const split = line.replace("+","").split(" ");
        instructions.push(<instruction>{
            op: split[0],
            arg: +split[1]
        });
    }
    return instructions;
}

type executionResult = { halts: boolean, acc: number };

function run(program: instruction[]): executionResult {
    const result = <executionResult> {
        acc: 0,
        halts: false
    };

    let pptr = 0;
    const seen = new Set<number>();

    while (true) {
        if (seen.has(pptr)) {
            result.halts = false;
            break;
        }

        if (pptr >= program.length) {
            result.halts = true;
            break;
        }

        seen.add(pptr);

        const instruction = program[pptr];

        switch (instruction.op) {
            case op.acc:
                result.acc += instruction.arg;
                pptr++;
                break;
            case op.jmp:
                pptr += instruction.arg;
                break;
            case op.nop:
                pptr++;
                break;
        }
    }

    return result;
}

function p1() {
    const program = parseInput();
    return run(program).acc;
}

function p2() {
    const program = parseInput();
    for (let i = 0; i < program.length; i++) {
        const modified = parseInput();
        switch (modified[i].op) {
            case op.acc: continue;
            case op.nop:
                modified[i].op = op.jmp;
                break;
            case op.jmp:
                modified[i].op = op.nop;
                break;
        }

        const result = run(modified);
        if (result.halts) {
            return result.acc;
        }
    }
}

timeAndPrint(p1);
timeAndPrint(p2);