function turnObjectIntoJsonString(arr) {
    let obj = {};
    for (let str of arr) {
        let objArgs = str.split(' -> ');
        let key = objArgs[0];
        let value = isNaN(Number(objArgs[1])) ? objArgs[1] : Number(objArgs[1]);

        obj[key] = value;
    }

    let jsonStr = JSON.stringify(obj);
    console.log(jsonStr);
}

turnObjectIntoJsonString(['name -> Angel', 'surname -> Georgiev', 'age -> 20',
    'grade -> 6.00', 'date -> 23/05/1995', 'town -> Sofia']);