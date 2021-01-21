
def parseInput()
    seats = []
    File.open("../inputs/D5.input", "r") do |f|
      for line in f.each_line
        seats.append(line.chomp)
      end
    end
    return seats
end

def fromBinary(s)
  s = s.gsub("L", "0")
       .gsub("R", "1")
       .gsub("F", "0")
       .gsub("B", "1")

   return Integer(s, 2)
end

def seatId(s)
  row = fromBinary(s[0...7])
  col = fromBinary(s[7..])
  return row * 8 + col
end

def p1()
  return parseInput().map {|s| seatId(s) }.max
end

def p2()
  ids = parseInput().map {|s| seatId(s) }.sort()
  for i in 0...ids.length
    return ids[i] + 1 if ids[i] + 1 != ids[i + 1]
  end
end

puts p1()
puts p2()