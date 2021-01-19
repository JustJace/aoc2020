from dataclasses import dataclass
from typing import List
import re

@dataclass
class baginfo:
    count: int
    name: str

@dataclass
class bag:
    name: str
    parents: List[str]
    children: List[baginfo]

def parseInput():
    file = open("../inputs/D7.input", "r")
    bags = {}
    contents = file.read()
    for line in contents.splitlines():
        split = line.split(" bags contain ")
        name = split[0]
        if name not in bags:
            bags[name] = bag(name, [], [])
        if split[1] == "no other bags.":
            continue
        
        for section in split[1].split(", "):
            groups = re.search("(\d+) (.+ .+) bags?\.?", section).groups()
            count = int(groups[0])
            childName = groups[1]
            if childName not in bags:
                bags[childName] = bag(childName, [], [])
            
            bags[name].children.append(baginfo(count, childName))
            bags[childName].parents.append(name)

    return bags

def countAncestry(bags, bag):
    stack = [bag.name]
    seen = set()
    while len(stack) > 0:
        current = stack.pop()
        if current in seen: continue
        seen.add(current)

        for parent in bags[current].parents:
            stack.append(parent)

    return len(seen) - 1

def p1():
    bags = parseInput()
    return countAncestry(bags, bags["shiny gold"])

def countLineage(bags, bag):
    count = 0
    for info in bag.children:
        count += info.count * (1 + countLineage(bags, bags[info.name]))
    return count

def p2():
    bags = parseInput()
    return countLineage(bags, bags["shiny gold"])


print(p1())
print(p2())
