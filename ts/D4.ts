import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

interface passport {
    byr?: string;
    iyr?: string;
    eyr?: string;
    hcl?: string;
    hgt?: string;
    ecl?: string;
    pid?: string;
    cid?: string;
}

const eye_colors = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];

function parseInput(): passport[] {
    const passports = [];
    const content = readFileSync('../inputs/D4.input','utf8');
    for (let group of content.split("\r\n\r\n")) {
        const passport = <passport>{};
        for (let line of group.split("\r\n")) {
            for (let field of line.split(" ")) {
                const kvp = field.split(":");
                passport[kvp[0]] = kvp[1];
            }
        }
        passports.push(passport);
    }
    return passports;
}

function validFields(passport: passport): boolean {
    const keys = Object.keys(passport).length;
    return keys == 8 || (keys == 7 && passport.cid === undefined);
}

function p1() {
    return parseInput()
        .filter(validFields)
        .length;
}

function validValues(passport: passport): boolean {
    if (+passport.byr < 1920 || +passport.byr > 2002)
        return false;

    if (+passport.iyr < 2010 || +passport.iyr > 2020)
        return false;

    if (+passport.eyr < 2020 || +passport.eyr > 2030)
        return false;

    if (passport.hgt.endsWith("cm")) {
        const cm = +passport.hgt.substr(0, passport.hgt.length - 2);
        if (cm < 150 || cm > 193)
            return false;
    } else if (passport.hgt.endsWith("in")) {
        const inches = +passport.hgt.substr(0, passport.hgt.length - 2);
        if (inches < 59 || inches > 76) 
            return false;
    } else {
        return false;
    }

    if (!passport.hcl.match(/^#[0-9a-f]{6}$/)) 
        return false;

    if (!eye_colors.includes(passport.ecl))
        return false;

    if (!passport.pid.match(/^[0-9]{9}$/))
        return false;

    return true;
}

function p2() {
    return parseInput()
        .filter(validFields)
        .filter(validValues)
        .length;
}

timeAndPrint(p1);
timeAndPrint(p2);