import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

type Instruction = string | Update;
interface Update {
    register: number;
    value: number;
}

function isUpdate(instruction: any): instruction is Update {
    return !!instruction.value;
}

function parseInput(): Instruction[] {
    const instructions = [];
    const content = readFileSync('../inputs/D14.input', 'utf8');
    for (let line of content.split('\r\n')) {
        const m = line.match(/(.+) = (.+)/);
        if (m[1] == 'mask') {
            instructions.push(m[2]);
        } else {
            const register = +m[1].match(/.+\[([0-9]+)\]/)[1];
            const value = +m[2];
            instructions.push(<Update>{
                register,
                value
            }); 
        } 
    }
    return instructions;
}

function sumRegisterValues(registers): number {
    let sum = 0;
    for (let key of Object.keys(registers)) {
        sum += registers[key];
    }
    return sum;
}

function applyMask(mask: string, value: number): number {
    let s = value.toString(2).padStart(36, '0');
    let b = '';
    
    for (let i = 0; i < 36; i++) {
        if (mask[i] == 'X') b += s[i];
        else                b += mask[i];
    }

    return parseInt(b, 2);
}

function p1() {
    const registers = {};
    let mask = '';
    for (let instruction of parseInput()) {
        if (isUpdate(instruction)) {
           registers[instruction.register] = applyMask(mask, instruction.value); 
        } else {
            mask = <string>instruction;
        }
    }
    return sumRegisterValues(registers);
}

function applyMaskFloating(mask: string, value: number): number[] {
    let s = value.toString(2).padStart(36, '0');
    let floats = [''];
    
    for (let i = 0; i < 36; i++) {
        if (mask[i] == 'X') {
            floats = floats.map(f => [f + '0', f + '1']).flat();
        } else if (mask[i] == '1') {
            floats = floats.map(f => f + '1');
        } else {
            floats = floats.map(f => f + s[i]);
        }
    }

    return floats.map(f => parseInt(f, 2));
}

function p2() {
    const registers = {};
    let mask = '';
    for (let instruction of parseInput()) {
        if (isUpdate(instruction)) {
            const floatedRegisters = applyMaskFloating(mask, instruction.register);
            for (let register of floatedRegisters)
                registers[register] = instruction.value;
        } else {
            mask = <string>instruction;
        }
    }
    return sumRegisterValues(registers);
}

timeAndPrint(p1);
timeAndPrint(p2);