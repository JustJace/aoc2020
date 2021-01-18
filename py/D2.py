from dataclasses import dataclass
import re

@dataclass
class policy:
    lo: int
    hi: int
    ch: str
    pw: str

def parseInput():
    file = open("../inputs/D2.input", "r")
    policies = []
    contents = file.read()
    for line in contents.splitlines():
        matchGroups = re.search("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)", line).groups()
        policies.append(policy(
            int(matchGroups[0]),
            int(matchGroups[1]),
            matchGroups[2],
            matchGroups[3]
        ))
    return policies

def p1():
    policies = parseInput()
    valid = 0
    for policy in policies:
        count = 0
        for char in policy.pw:
            if (char == policy.ch):
                count += 1
                if (count > policy.hi):
                    break
        
        if (count >= policy.lo and count <= policy.hi):
            valid += 1

    return valid
def p2():
    policies = parseInput()
    valid = 0
    for policy in policies:
        if ((policy.pw[policy.lo - 1] == policy.ch) 
         != (policy.pw[policy.hi - 1] == policy.ch)):
            valid += 1
    return valid

print(p1())
print(p2())