def parseInput
  nums = []
  File.open("../inputs/D10.input", "r") do |f|
    for line in f.each_line
      nums.append(Integer(line.chomp))
    end
  end
  return nums
end

def p1()
  jolts = parseInput
  jolts.append(0)
  jolts.append(jolts.max + 3)
  jolts.sort!

  ones = 0
  threes = 0
  for i in 0...jolts.length - 1
    dif = jolts[i+1] - jolts[i]
    ones += 1 if dif == 1
    threes += 1 if dif == 3
  end
  return ones * threes
end

def p2()
  jolts = parseInput
  jolts.append(0)
  jolts.append(jolts.max + 3)
  jolts.sort!
  dp = {}
  dp[jolts.length - 1] = 0
  dp[jolts.length - 2] = 1
  dp[jolts.length - 3] = 1
  for i in (jolts.length - 4).downto(0)
    dp[i] = 0
    dp[i] += dp[i+1] if jolts[i + 1] - jolts[i] <= 3
    dp[i] += dp[i+2] if jolts[i + 2] - jolts[i] <= 3
    dp[i] += dp[i+3] if jolts[i + 3] - jolts[i] <= 3
  end

  return dp[0]
end

puts p1()
puts p2()