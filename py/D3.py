
def parseInput():
    file = open("../inputs/D3.input", "r")
    map = []
    contents = file.read()
    for line in contents.splitlines():
        map.append(line)
    return map
    
def treeSlope(map, dr, dc):
    count = 0
    r = 0
    c = 0
    w = len(map[0])

    while r < len(map):
        if map[r][c%w] == '#':
            count += 1

        r += dr
        c += dc

    return count

def p1():
    map = parseInput()
    return treeSlope(map, 1, 3)

def p2():
    map = parseInput()
    return (
        treeSlope(map, 1, 1) 
      * treeSlope(map, 1, 3) 
      * treeSlope(map, 1, 5) 
      * treeSlope(map, 1, 7) 
      * treeSlope(map, 2, 1)
    )

print(p1())
print(p2())