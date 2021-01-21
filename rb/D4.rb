
def parseInput()
    passports = []
    current = {}
    File.open("../inputs/D4.input", "r") do |f|
      for line in f.each_line
        if line.chomp == ''
          passports.append(current)
          current = {}
        end
        for field in line.chomp.split(' ')
          kvp = field.split(':')
          current[kvp[0]] = kvp[1]
        end
      end
    end
    passports.append(current)
    return passports
end

def validateFields(passport)
  return passport.count == 8 || (passport.count == 7 && !passport.include?("cid"))
end

def p1()
  passports = parseInput()
  valid = 0
  for passport in passports
    valid += 1 if validateFields(passport)
  end
  return valid
end

$eye_colors = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]

def validateValues(passport)
  byr = Integer(passport["byr"])
  return false if byr < 1920 || byr > 2002

  iyr = Integer(passport["iyr"])
  return false if iyr < 2010 || iyr > 2020

  eyr = Integer(passport["eyr"])
  return false if eyr < 2020 || eyr > 2030

  hgt = passport["hgt"]
  if hgt[-2..] == "cm"
    cm = Integer(hgt[0...-2])
    return false if cm < 150 || cm > 193
  elsif hgt[-2..] == "in"
    inches = Integer(hgt[0...-2])
    return false if inches < 59 || inches > 76
  else
    return false
  end

  hcl = passport["hcl"]
  return false if !(hcl =~ /^#[0-9a-f]{6}$/)

  ecl = passport["ecl"]
  return false if !$eye_colors.include?(ecl)

  pid = passport["pid"]
  return false if !(pid =~ /^[0-9]{9}$/)

  return true
end

def p2()
  passports = parseInput()
  valid = 0
  for passport in passports
    valid += 1 if validateFields(passport) && validateValues(passport)
  end
  return valid
end

puts p1()
puts p2()