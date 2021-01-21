def parseInput
  nums = []
  File.open("../inputs/D9.input", "r") do |f|
    for line in f.each_line
      nums.append(Integer(line.chomp))
    end
  end
  return nums
end

def weakness(nums)
  for i in 25...nums.length
    found = false
    for j in i-25...i
      for k in j+1...i
        found = true if nums[i] == nums[j] + nums[k]
        break if found
      end
      break if found
    end
    return nums[i] if !found
  end
end

def p1()
  return weakness parseInput
end

def p2()
  nums = parseInput
  weakness = weakness(nums)
  left = 0
  right = 1
  sum = nums[0] + nums[1]
  while sum != weakness
    if sum > weakness
      sum -= nums[left]
      left += 1
    else
      right += 1
      sum += nums[right]
    end
  end
  return nums[left..right].minmax.sum
end

puts p1()
puts p2()