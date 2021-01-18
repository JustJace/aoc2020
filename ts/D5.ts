import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

function parseInput(): string[] {
    const seats = [];
    const content = readFileSync('../inputs/D5.input','utf8');
    for (let line of content.split("\r\n"))
        seats.push(line);
    return seats;
}

function binary(str: string): number {
    const binaryString = str
        .split('L').join('0')
        .split('R').join('1')
        .split('F').join('0')
        .split('B').join('1');

    return parseInt(binaryString, 2);
}

function seatId(seat: string): number {
    return binary(seat.substr(0, 7)) * 8 
         + binary(seat.substr(7));
}

function p1() {
    return parseInput()
        .map(seatId)
        .sort((a, b) => a - b)
        .pop();
}

function p2() {
    const ids = parseInput()
        .map(seatId)
        .sort((a, b) => a - b);
        
    for (let i = 0; i < ids.length; i++)
        if (ids[i] + 1 != ids[i + 1])
            return ids[i] + 1;
}

timeAndPrint(p1);
timeAndPrint(p2);