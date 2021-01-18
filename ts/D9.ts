import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

function parseInput(): number[] {
    const numbers = [];
    const content = readFileSync('../inputs/D9.input','utf8');
    for (let line of content.split("\r\n")) {
        numbers.push(+line);
    }
    return numbers;
}

function findWeakness(numbers: number[]): number {
    for (let i = 25; i < numbers.length; i++) {
        let found = false;
        for (let j = i - 25; j < i; j++) {
            for (let k = j + 1; k < i; k++) {
                if (numbers[j] + numbers[k] == numbers[i]) {
                    found = true;
                    break;
                }
            }
            if (found) break;
        }

        if (!found) return numbers[i];
    }
}

function p1() {
    return findWeakness(parseInput());
}

function p2() {
    const numbers = parseInput();
    const weakness = findWeakness(numbers);

    let L = 0, R = 1, sum = numbers[0] + numbers[1];

    while (sum != weakness) {
        if (sum > weakness) {
            sum -= numbers[L];
            L++;
        } else {
            R++;
            sum += numbers[R];
        }
    }

    const range = numbers.slice(L, R + 1);
    const min = Math.min(...range);
    const max = Math.max(...range);
    return min + max;
}

timeAndPrint(p1);
timeAndPrint(p2);