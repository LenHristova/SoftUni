function parseJsonObjects(arr) {
    for (let jsonStr of arr){
        let obj = JSON.parse(jsonStr);
        console.log('Name: ' + obj.name);
        console.log('Age: ' + obj.age);
        console.log('Date: ' + obj.date);
    }
}

parseJsonObjects(['{"name":"Gosho","age":10,"date":"19/06/2005"}',
    '{"name":"Tosho","age":11,"date":"04/04/2005"}']);
//parseJsonObjects(['{"name":"Gosho","age":10,"date":"19/06/2005"}']);