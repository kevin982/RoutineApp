let indexesCount = 1;
let currentIndex = 1;

const showMessage = (icon, title, errorMessage) => {
    Swal.fire({
        background: 'black',
        position: 'top-center',
        icon: `${icon}`,
        title: `${title}`,
        text: `${errorMessage}`,
        customClass: {
            title: 'text-white',
            text: 'text-white'
        }
    });
}


const addCategoriesToCombo = async () => {

    try {
        const result = await fetch("https://localhost:9000/v1/Categories");

        const response = await result.json();

        if (!response.succeeded) {
            throw new Error(`We could not achieve the categories due to ${response.title}`);
            return;
        }

        const categoriesCombo = document.getElementById("categoryCombo");

        const categories = response.content;

        for (let i = 0; i < categories.length; i++) {

            const option = document.createElement("option");
            option.setAttribute("value", categories[i].categoryId);
            option.textContent = categories[i].categoryName;

            categoriesCombo.appendChild(option);
        }
    } catch (e) {
        throw e;
    }
}

const getExercisesByCategoryAsync = async (categoryId, index, size) => {

    try {

        if (categoryId === undefined || index === undefined || size === undefined) return undefined;

        const resp = await fetch(`https://localhost:9000/v1/Exercise/Category/${categoryId}/${index}/${size}`);

        const result = await resp.json();

        if (result.succeeded) return result.content;

        if (result.statusCode === 404) return null;

        throw new Error(`We could not get the exercises due to ${result.title}`);

    } catch (e) {
        throw e;
    }

    
}

const eliminatePastExercisesFromTheDom = (container) => {

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }

}

const addTheExercisesToTheDom = (exercises) => {

    try {
        const container = document.getElementById("exercisesContainer");

        eliminatePastExercisesFromTheDom(container);

        for (let i = 0; i < exercises.length; i++) {

            let exercise = exercises[i];
 
            let divRow = document.createElement("div");
            divRow.className = "row justify-content-center mb-5";

            const card = document.createElement("div");
            card.className = "card bg-dark border border-light";
            card.style = "width: 35rem";

            const image = document.createElement("img");
            image.className = "card-img-top";
            image.setAttribute("src", exercise.imageUrl);

            const headerCard = document.createElement("div");
            headerCard.className = "card-body";

            const hr = document.createElement("hr");
            hr.className = "bg-white";

            const text = document.createElement("p");
            text.className = "text-white text-center fs-4";
            text.textContent = exercise.exerciseName;

            const cardBody = document.createElement("div");
            cardBody.className = "card-body";

            const rowButton = document.createElement("div");
            rowButton.className = "row justify-content-center";

            let action = (exercise.isInTheRoutine) ? "Remove" : "Add";

            let cardButton = document.createElement("button");
            cardButton.className = "btn btn-outline-light mt-4 rounded";
            cardButton.textContent = action;
            cardButton.setAttribute("id", "buttonExercise%" + action + "%" + exercise.id + "%" + exercise.exerciseName + "%" + document.getElementById("categoryCombo").options[document.getElementById("categoryCombo").selectedIndex].text + "%" + exercise.imageUrl);


            headerCard.appendChild(text);
            headerCard.appendChild(hr);

            rowButton.appendChild(cardButton);
            cardBody.appendChild(rowButton);

            card.appendChild(headerCard);
            card.appendChild(image);
            card.appendChild(cardBody);

            divRow.appendChild(card);

            container.appendChild(divRow);




        }
    } catch (e) {
        throw e;
    }

}

const thereAreNotExercises = () => {

    try {
        const container = document.getElementById("exercisesContainer");

        eliminatePastExercisesFromTheDom(container);

        const rowCard = document.createElement("div");
        rowCard.className = "row justify-content-center mb-5";

        const card = document.createElement("div");
        card.className = "card bg-dark border border-light";
        card.style = "width: 28rem";

        const image = document.createElement("img");
        image.className = "card-img-top";
        image.setAttribute("src", "/Images/SadFace.png");

        const bodyCard = document.createElement("div");
        bodyCard.className = "card-body";

        const hr = document.createElement("hr");
        hr.className = "bg-white";

        const text = document.createElement("p");
        text.className = "text-white text-center fs-4";
        text.textContent = "There are no exercises for the specific category";

        bodyCard.appendChild(text);
        bodyCard.appendChild(hr);

        card.appendChild(bodyCard);
        card.appendChild(image);

        rowCard.appendChild(card);

        container.appendChild(rowCard);
    } catch (e) {
        throw e;
    }
    
}

const getIndexes = async (categoryId, size) =>
{
    try {
        const resp = await fetch(`https://localhost:9000/v1/Exercise/IndexesCount/${categoryId}/${size}`);

        const result = await resp.json();

        return (result.succeeded) ? result.content.count : 0;

    } catch (e) {
        throw e;
    }
}

const enableDisablePrivousNextBtns = () => {

    try {
        document.getElementById("btnPrevious").className = "btn btn-outline-light";
        document.getElementById("btnNext").className = "btn btn-outline-light";
        document.getElementById("index").textContent = currentIndex;

        if (indexesCount === 0) {
            document.getElementById("btnPrevious").className = "btn btn-outline-light disabled";
            document.getElementById("btnNext").className = "btn btn-outline-light disabled";
            document.getElementById("index").textContent = "0";
            currentIndex = 0;
        }

        if (currentIndex === 1) {
            document.getElementById("btnPrevious").className = "btn btn-outline-light disabled";
        }

        if (currentIndex === indexesCount) {
            document.getElementById("btnNext").className = "btn btn-outline-light disabled";
        }
    } catch (e) {
        throw e;
    }
}

window.addEventListener("load", async () => {

    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        await addCategoriesToCombo();

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";

    } catch (e) {
        showMessage('error', 'Error!', e.message);
    }

});

document.getElementById("categoryCombo").addEventListener("change", async () => {

    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        currentIndex = 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    } catch (e) {
        showMessage('error', 'Error!', e.message);
    }

});

document.getElementById("size").addEventListener("change", async () => {

    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        currentIndex = 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    } catch (e) {
        showMessage('error', 'Error!', e.message);
    }
});

document.getElementById("btnPrevious").addEventListener("click", async () => {

    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        currentIndex -= 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";

    } catch (e) {
        showMessage('error', 'Error!', e.message);
    }
});

document.getElementById("btnNext").addEventListener("click", async () => {

    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        currentIndex += 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    } catch (e) {
        showMessage('error', 'Error!', e.message);
    }

    
});
 
document.addEventListener("click", async (e) => {

    let id = e.target.id;

    let [elementName, action, exerciseId, exerciseName, categoryName, imageUrl] = id.split('%');

    if (elementName !== 'buttonExercise') return;

    (action === "Add") ? await AddExercise({ exerciseId: exerciseId, exerciseName: exerciseName, categoryName: categoryName, imageUrl: imageUrl }) : await RemoveExercise(exerciseId);
 
});

const AddExercise = async (exercise) => {
     
    Swal.fire({
        background: 'black',
        title: 'Add Exercise',
        html: `
                
                <label class = 'text-white mb-3'>Choose sets number</label>
                <br />
                <input type="number" id="sets" class="swal2-input bg-dark text-white mb-3" placeholder="Sets number">
                <br />
                <label class = 'text-white mb-3'>Choose the day you want to train it</label>
                <br />
                <select multiple class = 'bg-dark text-white' id = 'daysToTrain'>
                    <option value = '1'>Monday</option>
                    <option value = '2'>Tuesday</option>
                    <option value = '3'>Wednesday</option>
                    <option value = '4'>Thursday</option>
                    <option value = '5'>Friday</option>
                    <option value = '6'>Saturday</option>
                    <option value = '7'>Sunday</option>
                </select>`,
        confirmButtonText: 'Add',
        focusConfirm: false,
        preConfirm: async () => {
            const sets = Swal.getPopup().querySelector('#sets').value;
            const options = document.getElementById('daysToTrain').selectedOptions;
            const days = Array.from(options).map(({ value }) => value);

            if (!sets || days.length === 0) {
                Swal.showValidationMessage(`Please enter sets and days`);
            } else if (sets === "0") {
                Swal.showValidationMessage(`Please enter sets and days`);
            } else {
                for (var i = 0; i < days.length; i++) days[i] = parseInt(days[i]);

                const addExerciseModel = {
                    exerciseId: exercise.exerciseId,
                    days: days,
                    sets: parseInt(sets),
                    exerciseName: exercise.exerciseName,
                    categoryName: exercise.categoryName,
                    imageUrl: exercise.imageUrl
                };

                const settings = {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(addExerciseModel)
                };


                const response = await fetch("https://localhost:9000/v1/Routine", settings);

                return await response.json();
            }

            

            
        }
    }).then( async (result) => {

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        (result.value.succeeded) ? showMessage('success', 'Exercises added!', 'The exercise has been added to your routine successfully!') : showMessage('error', 'Error!', `The exercise could not added due to ${result.value.title}`);



    })
     
}

const RemoveExercise = async (id) => {
     
    Swal.fire({
        title: 'Are you sure?',
        background: 'black',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then(async (result) => {

        if (result.isConfirmed) {

            const settings = {
                method: 'DELETE'
            };

            const uri = `https://localhost:9000/v1/Routine/${id}`;

            const response = await fetch(`https://localhost:9000/v1/Routine/${id}`, settings);

            const content = await response.json();

            (content.succeeded) ? showMessage('success', 'Exercise removed!', 'The exercise has been removed from routine successfully!') : showMessage('error', 'Error!', `The exercise could not be removed from routine due to ${content.title}`);

            document.getElementById("index").textContent = currentIndex;

            const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

            (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

            indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

            enableDisablePrivousNextBtns();
        }
    })
     
}
 