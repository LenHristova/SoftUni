function setValuesToIndexes(arr) {
    let arrLength = Number(arr[0]);

    let res = new Array(arrLength).fill(0);

    for (let i = 1; i < arr.length; i++) {
        let indexAndValue = arr[i]
            .split(' - ')
            .map(Number);
        let index = indexAndValue[0];
        let value = indexAndValue[1];

        res[index] = value;
    }

    for (let num of res) {
        console.log(num)
    }
}

setValuesToIndexes(['5', '0 - 3', '-1 - -1', '4 - 2']);