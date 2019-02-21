# Question

[Find Position](https://www.codewars.com/kata/582c1092306063791c000c00/train/csharp)

## Instructions

**Description**

There is a infinite string. You can imagine it's a combination of numbers from 1 to n, like this:

 "123456789101112131415....n-2n-1n"

Please note: the length of the string is infinite. It depends on how long you need it(I can't offer it as a argument, it only exists in your imagination) ;-)

Your task is complete function findPosition that accept a digital string num. Returns the position(index) of the digital string(the first appearance).

For example:

 findPosition("456") == 3
 because "123456789101112131415".indexOf("456") = 3
             ^^^

Is it simple? No, It is more difficult than you think ;-)

 findPosition("454") = ?
 Oh, no! There is no "454" in "123456789101112131415",
 so we should return -1?
 No, I said, this is a string of infinite length.
 We need to increase the length of the string to find "454"

 findPosition("454") == 79
 because "123456789101112131415...44454647".indexOf("454")=79
                                    ^^^

The length of argument num is 2 to 15. So now there are two ways: one is to create a huge own string to find the index position; Or thinking about an algorithm to calculate the index position.

Which way would you choose? ;-)

Some examples:

 findPosition("456") == 3
 ("...3456...")
       ^^^
 findPosition("454") == 79
 ("...444546...")
        ^^^
 findPosition("455") == 98
 ("...545556...")
       ^^^
 findPosition("910") == 8
 ("...7891011...")
        ^^^
 findPosition("9100") == 188
 ("...9899100101...")
         ^^^^
 findPosition("99100") == 187
 ("...9899100101...")
        ^^^^^
 findPosition("00101") == 190
 ("...99100101...")
         ^^^^^
 findPosition("001") == 190
 ("...9899100101...")
           ^^^
 findPosition("123456789") == 0
 findPosition("1234567891") == 0
 findPosition("123456798") == 1000000071
A bit difficulty, A bit of fun, happy coding ;-)