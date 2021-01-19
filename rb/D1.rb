
def parseInput()
    numbers = []
    File.open("../inputs/D1.input", "r") do |f|
        f.each_line do |line|
          numbers.append(Integer(line))
        end
      end
    return numbers
end

def p1()
    numbers = parseInput().sort()
    left = 0
    right = numbers.length() - 1
    while numbers[left] + numbers[right] != 2020
      if numbers[left] + numbers[right] > 2020
        right -= 1
      else
        left += 1
      end
    end
    return numbers[left] * numbers[right]
end

def p2()
  numbers = parseInput().sort()
  
  for i in 0...numbers.length()
    remainder = 2020 - numbers[i]
    left = i + 1
    right = numbers.length() - 1
    while left < right 
      if numbers[left] + numbers[right] == remainder
        return numbers[left] * numbers[right] * numbers[i]
      elsif numbers[left] + numbers[right] > remainder
        right -= 1
      else
        left += 1
      end
    end
  end
end

puts p1()
puts p2()