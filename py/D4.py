import re

def parseInput():
    file = open("../inputs/D4.input", "r")
    passports = []
    contents = file.read()
    for section in contents.split("\n\n"):
        passport = {}
        for line in section.splitlines():
            for field in line.split(" "):
                kvp = field.split(":")
                passport[kvp[0]] = kvp[1]
        passports.append(passport)
    return passports

def validateFields(passport: dict):
    return len(passport.keys()) == 8 or (len(passport.keys()) == 7 and "cid" not in passport.keys())
    
def p1():
    return len(list(filter(validateFields, parseInput())))

eye_colors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }

def validateValues(passport: dict):
    byr = int(passport["byr"])
    if (byr < 1920 or byr > 2002): return False

    iyr = int(passport["iyr"])
    if (iyr < 2010 or iyr > 2020): return False

    eyr = int(passport["eyr"])
    if (eyr < 2020 or eyr > 2030): return False

    hgt = str(passport["hgt"])
    if hgt.endswith("cm"):
        cm = int(hgt[0:len(hgt)-2])
        if cm < 150 or cm > 193: return False
    elif hgt.endswith("in"):
        inches = int(hgt[0:len(hgt)-2])
        if inches < 59 or inches > 76: return False
    else:
        return False

    hcl = passport["hcl"]
    if re.match("^#[0-9a-f]{6}$", hcl) == None: return False

    ecl = passport["ecl"]
    if ecl not in eye_colors: return False

    pid = passport["pid"]
    if re.match("^[0-9]{9}$", pid) == None: return False

    return True

def p2():
    return len(list(filter(validateValues, filter(validateFields, parseInput()))))

print(p1())
print(p2())