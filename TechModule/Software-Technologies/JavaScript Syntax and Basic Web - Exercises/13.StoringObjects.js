function storeObjects(arr) {
    let students = [];
    for (let str of arr) {
        let studentInfo = str.split(' -> ');
        let name = studentInfo[0];
        let age = Number(studentInfo[1]);
        let grade = Number(studentInfo[2]).toFixed(2);

        students.push({name: name, age: age, grade: grade});
    }

    students.forEach(st => console.log(`Name: ${st.name}\nAge: ${st.age}\nGrade: ${st.grade}`));
}

storeObjects(['Pesho -> 13 -> 6.00', 'Ivan -> 12 -> 5.57', 'Toni -> 13 -> 4.90']);