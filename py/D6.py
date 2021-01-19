

def parseInput():
    file = open("../inputs/D6.input", "r")
    groups = []
    contents = file.read()
    for section in contents.split("\n\n"):
        group = []
        for person in section.splitlines():
            group.append(person)
        groups.append(group)
    return groups

def questionsAnswered(groups):
    count = 0
    for group in groups:
        seen = set()
        for person in group:
            for question in person:
                seen.add(question)
        count += len(seen)
    return count

def p1():
    return questionsAnswered(parseInput())

def unamiouslyAnswered(groups):
    count = 0
    for group in groups:
        seen = {}
        for person in group:
            for question in person:
                if question not in seen:
                    seen[question] = 0
                seen[question] += 1
        count += len(dict(filter(lambda kvp: kvp[1] == len(group), seen.items())))
    return count

def p2():
    return unamiouslyAnswered(parseInput())


print(p1())
print(p2())
