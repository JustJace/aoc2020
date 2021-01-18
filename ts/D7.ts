import { info } from 'console';
import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

class bag {
    name: string;
    parents: string[];
    children: baginfo[];
}

class baginfo {
    name: string;
    count: number;
}

type bagmap = { [name: string]: bag };

function parseInput(): bagmap {
    const bags = <bagmap>{};
    const content = readFileSync('../inputs/D7.input','utf8');
    for (let line of content.split("\r\n")) {
        const split = line.split(" bags contain ");
        const parentName = split[0];
        if (!bags[parentName])
            bags[parentName] = <bag>{ name: parentName, parents: [], children: [] };
        
        if (split[1] == 'no other bags.') continue;

        for (let cs of split[1].split(', ')) {
            const match = cs.match(/(\d+) (.+) bags?\.?/)
            const count = +match[1];
            const childName = match[2];
            if (!bags[childName])
                bags[childName] = <bag>{ name: childName, parents: [], children: [] };
            
            bags[parentName].children.push(<baginfo>{
                name: childName,
                count
            });

            bags[childName].parents.push(parentName);
        }
    }
    return bags;
}

function countAncestryDFS(bags: bagmap, bag: bag): number {
    const seen = new Set<string>();
    const stack = [bag.name];
    while (stack.length) {
        const current = stack.pop();
        if (seen.has(current)) continue;
        seen.add(current);
        bags[current].parents.forEach(p => stack.push(p));
    }
    return seen.size - 1;
}

function p1() {
    const bags = parseInput();
    return countAncestryDFS(bags, bags["shiny gold"]);
}

function countLineageRecursive(bags: bagmap, bag: bag): number {
    let count = 0;

    for (let info of bag.children)
        count += info.count * ( 1 + countLineageRecursive(bags, bags[info.name]));

    return count;
}

function p2() {
    const bags = parseInput();
    return countLineageRecursive(bags, bags["shiny gold"]);
}

timeAndPrint(p1);
timeAndPrint(p2);