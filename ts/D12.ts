import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

enum heading { N, E, S, W };
enum action { N = "N", E = "E", S = "S", W = "W", L = "L", R = "R", F = "F" };
type nav = { action: action, value: number };

const heading_degrees_map = {
    [heading.E]: 0,
    [heading.N]: 90,
    [heading.W]: 180,
    [heading.S]: 270
};

const degrees_heading_map = {
    [0]: heading.E,
    [90]: heading.N,
    [180]: heading.W,
    [270]: heading.S
};

function parseInput(): nav[] {
    const navs = [];
    const content = readFileSync('../inputs/D12.input', 'utf8');
    for (let line of content.split("\r\n")) {
        navs.push(<nav>{
            action: line[0],
            value: +line.substr(1)
        });
    }
    return navs;
}

function changeHeading(h: heading, degrees: number): heading {
    return degrees_heading_map[(heading_degrees_map[h] + degrees) % 360];
}

function applyNav(nav: nav, x: number, y: number, h: heading): { x: number, y: number, h: heading } {
    switch (nav.action) {
        case action.N: y -= nav.value; break;
        case action.S: y += nav.value; break;
        case action.W: x -= nav.value; break;
        case action.E: x += nav.value; break;
        case action.L: h = changeHeading(h, nav.value); break;
        case action.R: h = changeHeading(h, 360 - nav.value); break;
        case action.F:
            switch (h) {
                case heading.N: y -= nav.value; break;
                case heading.S: y += nav.value; break;
                case heading.W: x -= nav.value; break;
                case heading.E: x += nav.value; break;
            }
        break;
    }

    return { x, y, h };
}

function p1() {
    let x = 0, y = 0, h = heading.E;

    for (let nav of parseInput()) {
        ({ x, y, h } = applyNav(nav, x, y, h));
    }

    return Math.abs(x) + Math.abs(y);
}

const rotate_map = {
    [90]:  (wx: number, wy: number) => ({wx: -wy, wy:  wx}),
    [180]: (wx: number, wy: number) => ({wx: -wx, wy: -wy}),
    [270]: (wx: number, wy: number) => ({wx:  wy, wy: -wx}),
    [360]: (wx: number, wy: number) => ({wx:  wx, wy:  wy})
}

function applyNav2(nav: nav, x: number, y: number, wx: number, wy: number): { x: number, y: number, wx: number, wy: number } {

    switch (nav.action) {
        case action.N: wy -= nav.value; break;
        case action.S: wy += nav.value; break;
        case action.W: wx -= nav.value; break;
        case action.E: wx += nav.value; break;
        case action.L: ({wx, wy} = rotate_map[360 - nav.value](wx, wy)); break;
        case action.R: ({wx, wy} = rotate_map[nav.value](wx, wy)); break;
        case action.F: x += wx * nav.value; y += wy * nav.value; break;
    }

    return { x, y, wx, wy };
}

function p2() {
    let x = 0, y = 0, wx = 10, wy = -1;

    for (let nav of parseInput()) {
        ({ x, y, wx, wy } = applyNav2(nav, x, y, wx, wy));
    }

    return Math.abs(x) + Math.abs(y);
}

timeAndPrint(p1);
timeAndPrint(p2);