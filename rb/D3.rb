
def parseInput()
    map = []
    File.open("../inputs/D3.input", "r") do |f|
      for line in f.each_line
        map.append(line.chomp)
      end
    end
    return map
end

def treeSlope(map, dr, dc)
  count = 0
  r = 0
  c = 0
  w = map[0].length
  while r < map.length
    count += 1 if map[r][c % w] == '#'
    r += dr
    c += dc
  end
  return count
end

def p1()
  return treeSlope(parseInput(), 1, 3)
end

def p2()
  return treeSlope(parseInput(), 1, 1) * 
         treeSlope(parseInput(), 1, 3) * 
         treeSlope(parseInput(), 1, 5) * 
         treeSlope(parseInput(), 1, 7) * 
         treeSlope(parseInput(), 2, 1)
end

puts p1()
puts p2()