require 'set'

def parseInput()
    groups = []
    current = []
    File.open("../inputs/D6.input", "r") do |f|
      for line in f.each_line
        if line.chomp == ''
          groups.append(current)
          current = []
          next
        end
        current.append(line.chomp)
      end
    end
    groups.append(current)
    return groups
end

def questionsAnswered(group)
  answered = Set[]
  for person in group
    for question in person.each_char
      answered.add(question)
    end
  end
  return answered.count
end

def p1()
  return parseInput().map {|g| questionsAnswered(g) }.sum()
end

def unamiouslyAnswered(group)
  answered = {}
  for person in group
    for question in person.each_char
      answered[question] = 0 if !answered.include? question
      answered[question] += 1
    end
  end
  return answered.select{|key, value| value == group.count}.count
end

def p2()
  return parseInput().map {|g| unamiouslyAnswered(g) }.sum()
end

puts p1()
puts p2()