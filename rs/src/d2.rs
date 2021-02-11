use regex::Regex;
use std::fs;

struct Policy {
    lo: i32,
    hi: i32,
    ch: char,
    pw: String,
}

pub fn p1() -> i32 {
    let policies = parse_input();
    let mut valid = 0;

    for policy in policies {
        let mut count = 0;

        for ch in policy.pw.chars() {
            if ch == policy.ch {
                count += 1;
            }
        }

        if count >= policy.lo && count <= policy.hi {
            valid += 1;
        }
    }

    return valid;
}

pub fn p2() -> i32 {
    let policies = parse_input();
    let mut valid = 0;

    for policy in policies {
        let lo_match = policy.ch == policy.pw.chars().nth((policy.lo - 1) as usize).unwrap();
        let hi_match = policy.ch == policy.pw.chars().nth((policy.hi - 1) as usize).unwrap();
        if lo_match ^ hi_match {
            valid += 1;
        }
    }

    return valid;
}

fn parse_input() -> Vec<Policy> {
    let contents = fs::read_to_string("../inputs/D2.input").unwrap();
    
    let mut policies = Vec::new();
    let regex = Regex::new("^([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)$").unwrap();
    for line in contents.split("\r\n") {
        let capture = regex.captures(line).unwrap();
        let lo = capture[1].parse::<i32>().unwrap();
        let hi = capture[2].parse::<i32>().unwrap();
        let ch = capture[3].chars().nth(0).unwrap();
        let pw = capture[4].to_string();
        policies.push(Policy
        {
            lo: lo,
            hi: hi,
            ch: ch,
            pw: pw
        });
    }

    return policies;
}