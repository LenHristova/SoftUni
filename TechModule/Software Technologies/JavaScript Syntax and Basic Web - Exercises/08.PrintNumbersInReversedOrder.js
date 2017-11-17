function printNumbersInReversedOrder(arr) {
    let numbers = arr
        .map(Number)
        .reverse();

    for (let num of numbers){
        console.log(num);
    }
}
printNumbersInReversedOrder(['5', '5.5', '24', '-3']);