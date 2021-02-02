use std::fs;

pub fn p1() -> i32 {
    let mut nums = parse_input();
    nums.sort();
    let mut left = 0; 
    let mut right = nums.len() - 1;
    while nums[left] + nums[right] != 2020 {
        if nums[left] + nums[right] > 2020 {
            right -= 1;
        }
        else {
            left += 1;
        }
    }
    return nums[left] * nums[right];
}

pub fn p2() -> i32 {
    let mut nums = parse_input();
    nums.sort();
    
    for (i, n) in nums.iter().enumerate() {
        let remainder = 2020 - n;
        let mut left = i + 1;
        let mut right = nums.len() - 1;
        while left < right {
            if nums[left] + nums[right] == remainder {
                return nums[left] * nums[right] * n;
            } else if nums[left] + nums[right] > remainder {
                right -= 1;
            } else {
                left += 1;
            }
        }
    }

    return -1;
}

fn parse_input() -> Vec<i32> {
    let contents = 
        fs::read_to_string("../inputs/D1.input")
        .expect("read file");

    let mut nums = Vec::new();

    for s in contents.split("\r\n") {
        nums.push(s.parse::<i32>().expect("parse int"));
    }

    return nums;
}