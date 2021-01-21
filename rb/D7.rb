require 'set'

BagInfo = Struct.new(:name, :count)
Bag = Struct.new(:name, :parents, :children)

def parseInput()
  bags = {}
  File.open("../inputs/D7.input", "r") do |f|
    for line in f.each_line
      line.chomp!
      split = line.split(" bags contain ")
      parent = split[0]
      bags[parent] = Bag.new(parent, [], []) if !bags.include? parent
      next if split[1] == "no other bags."
      for c in split[1].split(", ")
        c =~ /(\d+) (.+) bags?\.?/
        child = $2
        bags[child] = Bag.new(child, [], []) if !bags.include? child
        bags[child].parents.append(parent)
        bags[parent].children.append(BagInfo.new(child, Integer($1)))
      end
    end
  end
  return bags
end

def countAncestry(bags, bag)
  seen = Set[]
  queue = [bag.name]

  while queue.length > 0
    current = queue.pop()
    next if seen.include? current
    seen.add current
    for parent in bags[current].parents
      queue.append parent
    end
  end

  return seen.count - 1
end

def p1()
  bags = parseInput()
  return countAncestry bags, bags["shiny gold"]
end

def countLineage(bags, bag)
  count = 0

  for info in bag.children
    count += info.count * (1 + countLineage(bags, bags[info.name]))
  end

  return count
end

def p2()
  bags = parseInput()
  return countLineage bags, bags["shiny gold"]
end

puts p1()
puts p2()