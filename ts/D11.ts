import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

enum Tile {
    occupied = '#',
    empty = 'L',
    floor = '.'
}

enum AdjacencyMode {
    visible,
    direct
}

function parseInput(): Tile[][] {
    const layout = [];
    const content = readFileSync('../inputs/D11.input', 'utf8');
    for (let line of content.split("\r\n")) {
        layout.push(line.split(''));
    }
    return layout;
}

function countAdjacentOccupiedChairs(layout: Tile[][], row: number, col: number): number {
    const up = row - 1;
    const down = row + 1;
    const left = col - 1;
    const right = col + 1;
    const height = layout.length;
    const width = layout[0].length;

    let count = 0;

    if (up >= 0 && layout[up][col] == Tile.occupied) count++;
    if (up >= 0 && left >= 0 && layout[up][left] == Tile.occupied) count++;
    if (up >= 0 && right < width && layout[up][right] == Tile.occupied) count++;

    if (left >= 0 && layout[row][left] == Tile.occupied) count++;
    if (right < width && layout[row][right] == Tile.occupied) count++;

    if (down < height && layout[down][col] == Tile.occupied) count++;
    if (down < height && left >= 0 && layout[down][left] == Tile.occupied) count++;
    if (down < height && right < width && layout[down][right] == Tile.occupied) count++;

    return count;
}

function hasOccupiedChairInDirection(layout: Tile[][], row: number, col: number, dr: number, dc: number): boolean {
    const height = layout.length;
    const width = layout[0].length;
    const r = row + dr;
    const c = col + dc;

    if (r < 0 || r >= height || c < 0 || c >= width) return false;

    switch (layout[r][c]) {
        case Tile.floor:
            return hasOccupiedChairInDirection(layout, r, c, dr, dc);
        case Tile.occupied:
            return true;
        case Tile.empty:
            return false;
    }
}

function countVisibleOccupiedChairs(layout: Tile[][], row: number, col: number): number {
    let count = 0;

    if (hasOccupiedChairInDirection(layout, row, col, -1, -1)) count++;
    if (hasOccupiedChairInDirection(layout, row, col, -1,  0)) count++;
    if (hasOccupiedChairInDirection(layout, row, col, -1, +1)) count++;

    if (hasOccupiedChairInDirection(layout, row, col,  0, -1)) count++;
    if (hasOccupiedChairInDirection(layout, row, col,  0, +1)) count++;

    if (hasOccupiedChairInDirection(layout, row, col, +1, -1)) count++;
    if (hasOccupiedChairInDirection(layout, row, col, +1,  0)) count++;
    if (hasOccupiedChairInDirection(layout, row, col, +1, +1)) count++;

    return count;
}

function shouldVacateChair(layout: Tile[][], r: number, c: number, mode: AdjacencyMode): boolean {
    switch (mode) {
        case AdjacencyMode.direct:
            return countAdjacentOccupiedChairs(layout, r, c) >= 4;
        case AdjacencyMode.visible:
            return countVisibleOccupiedChairs(layout, r, c) >= 5;
    }
}

function shouldFillChair(layout: Tile[][], r: number, c: number, mode: AdjacencyMode): boolean {
    switch (mode) {
        case AdjacencyMode.direct:
            return countAdjacentOccupiedChairs(layout, r, c) == 0;
        case AdjacencyMode.visible:
            return countVisibleOccupiedChairs(layout, r, c) == 0;
    }
}

function iterate(layout: Tile[][], mode: AdjacencyMode): {changed: number, next: Tile[][]} {
    let changed = 0;
    let next = layout.map(r => [...r]);
    for (let r = 0; r < layout.length; r++) {
        for (let c = 0; c < layout[r].length; c++) {
            switch (layout[r][c]) {
                case Tile.floor: continue;
                case Tile.occupied:
                    if (shouldVacateChair(layout, r, c, mode)) {
                        next[r][c] = Tile.empty;
                        changed++;
                    }
                break;
                case Tile.empty:
                    if (shouldFillChair(layout, r, c, mode)) {
                        next[r][c] = Tile.occupied;
                        changed++;
                    }
                break;
            }
        }
    }
    return { changed, next };
}

function countOccupied(layout: Tile[][]): number {
    let count = 0;
    for (let row of layout)
    for (let tile of row)
        if (tile == Tile.occupied)
            count++;
    return count;
}

function p1() {
    let layout = parseInput();
    while (true) {
        let {changed, next} = iterate(layout, AdjacencyMode.direct);
        layout = next;
        if (changed == 0) break;
    }
    return countOccupied(layout);
}

function p2() {
    let layout = parseInput();
    while (true) {
        let {changed, next} = iterate(layout, AdjacencyMode.visible);
        layout = next;
        if (changed == 0) break;
    }
    return countOccupied(layout);
}

timeAndPrint(p1);
timeAndPrint(p2);