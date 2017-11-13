function findThreeIntegersSum([input]) {
    let arr = input.split(' ').map(Number);
    let a = arr[0];
    let b = arr[1];
    let c = arr[2];

    if (a + b == c) {
        console.log(`${Math.min(a, b)} + ${Math.max(a, b)} = ${c}`);
    } else if (a + c == b) {
        console.log(`${Math.min(a, c)} + ${Math.max(a, c)} = ${b}`);
    } else if (b + c == a) {
        console.log(`${Math.min(b, c)} + ${Math.max(b, c)} = ${a}`);
    } else {
        console.log("No");
    }
}
findThreeIntegersSum(['8 15 7']);