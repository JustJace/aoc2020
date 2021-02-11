use std::fs;

pub fn p1() -> usize {
   return tree_slope(&parse_input(), 1, 3);
}

pub fn p2() -> usize {
   let map = parse_input();
   return tree_slope(&map, 1, 1)
        * tree_slope(&map, 1, 3)
        * tree_slope(&map, 1, 5)
        * tree_slope(&map, 1, 7)
        * tree_slope(&map, 2, 1);
}

fn tree_slope(map: &Vec<Vec<char>>, dr: usize, dc: usize) -> usize {
    let mut r = 0;
    let mut c = 0;
    let mut trees = 0;
    let width = map[0].len();

    while r < map.len() {
        let current = map[r][c % width];

        if current == '#' {
            trees += 1;
        }

        r += dr;
        c += dc;
    }

    return trees;
}

fn parse_input() -> Vec<Vec<char>> {
    let contents = fs::read_to_string("../inputs/D3.input").unwrap();
    let mut map = Vec::new();

    for line in contents.split("\r\n") {
       map.push(line.chars().collect());
    }

    return map;
}