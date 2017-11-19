function findLargest3Numbers(input) {
    let arr = input
        .map(Number)
        .sort((a,b) => b-a)
        .slice(0, 3);
    for(let num of arr) {
        console.log(num)
    }
}

findLargest3Numbers([10, 30]);
