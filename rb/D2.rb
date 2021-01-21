
Policy = Struct.new(:lo, :hi, :ch, :pw)

def parseInput()
    policies = []
    File.open("../inputs/D2.input", "r") do |f|
        f.each_line do |line|
          line =~ /(\d+)-(\d+) ([a-z]): ([a-z]+)/
          policies.append(Policy.new(Integer($1), Integer($2), $3, $4))
        end
      end
    return policies
end

def p1()
  policies = parseInput()
  valid = 0
  for policy in policies
    count = 0
    for char in policy.pw.each_char
      count += 1 if char == policy.ch
      break if count > policy.hi
    end
    valid += 1 if count >= policy.lo && count <= policy.hi
  end
  return valid
end

def p2()
  policies = parseInput()
  valid = 0
  for policy in policies
    valid += 1 if (policy.pw[policy.lo - 1] == policy.ch) ^ (policy.pw[policy.hi - 1] == policy.ch)
  end
  return valid
end

puts p1()
puts p2()