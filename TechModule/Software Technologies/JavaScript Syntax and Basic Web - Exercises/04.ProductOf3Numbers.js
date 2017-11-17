function isProductPositive(arr) {
    let num1 = Number(arr[0]);
    let num2 = Number(arr[1]);
    let num3 = Number(arr[2]);

    if (num1 === 0 || num2 === 0 || num3 === 0) {
        console.log('Positive');
    } else if (countNegativeNumbers(arr) % 2 !== 0) {
        console.log('Negative');
    } else {
        console.log('Positive');
    }

    function countNegativeNumbers(arr) {
        let negativeNumbersCount = 0;
        for (let num of arr) {
            if (Number(num) < 0) {
                negativeNumbersCount++;
            }
        }
        return negativeNumbersCount;
    }
}

isProductPositive(['2', '3', '-1']);