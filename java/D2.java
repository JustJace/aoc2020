import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

class policy {
    int low;
    int high;
    char chr;
    String password;
}

class D2 {
    public static void main(String[] args) throws Exception {
        System.out.println(p1());
        System.out.println(p2());
    }

    public static int p1() throws Exception {
        var valid = 0;
        for ( var policy : parseInput() ) {
            var count = 0;

            for (var chr : policy.password.toCharArray())
                if (chr == policy.chr)
                    count++;

            if (count >= policy.low && count <= policy.high)
                valid++;
        }
        return valid;
    }

    public static int p2() throws Exception {
        var valid = 0;
        for (var policy : parseInput()) {
            if (policy.chr == policy.password.toCharArray()[policy.low - 1]
              ^ policy.chr == policy.password.toCharArray()[policy.high - 1]
            ) {
                valid++;
            }
        }

        return valid;
    }

    public static ArrayList<policy> parseInput() throws Exception {
        ArrayList<policy> policies = new ArrayList<policy>();
        var regex = Pattern.compile("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");
        for (var line : Files.readAllLines(Path.of("../inputs/D2.input")))
        {
            var match = regex.matcher(line);
            match.find();
            policies.add(new policy()
            {{
                low = Integer.parseInt(match.group(1));
                high = Integer.parseInt(match.group(2));
                chr = match.group(3).toCharArray()[0];
                password = match.group(4);
            }});
        }
        return policies;
    }
}