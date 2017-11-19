function setKeyValue(arr) {

    let searchedKey = arr.pop();

    let obj = {};
    for (let str of arr) {
        let keyValue = str.split(' ');
        let key = keyValue[0];
        let value = keyValue[1];
        obj[key] = value;

    }
    console.log(obj[searchedKey] === undefined ? 'None' : obj[searchedKey]);
}

//multiplyValuesForKey(['key value', 'key eulav', 'test tset', 'key']);
//multiplyValuesForKey(['3 test', '3 test1', '4 test2', '4 test3', '4 test5', '4']);
setKeyValue(['3 bla', '3 alb', '2']);