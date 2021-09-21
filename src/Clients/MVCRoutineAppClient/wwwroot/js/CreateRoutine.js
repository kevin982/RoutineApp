let indexesCount = 1;
let currentIndex = 1;

const showMessage = (icon, title, errorMessage) => {
    Swal.fire({
        position: 'top-center',
        icon: `${icon}`,
        title: `${title}`,
        text: `${errorMessage}`,
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

        if (!result.succeeded) throw new Error(`We could not get the exercises due to ${result.title}`);

        return result.content;

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

            console.log(exercise);

            let divRow = document.createElement("div");
            divRow.className = "row justify-content-center";

            let card = document.createElement("div");
            card.className = "card col-8 mb-5 bg-dark";

            let cardImage = document.createElement("img");
            cardImage.className = "card-img-top";
            cardImage.setAttribute("src", exercise.imageUrl);

            let cardBody = document.createElement("div");
            cardBody.className = "card-body gradient-background";

            let cardTitle = document.createElement("h2");
            cardTitle.className = "card-title text-white mt-3";
            cardTitle.textContent = exercise.exerciseName;

            let action = (exercise.isInTheRoutine) ? "Remove" : "Add";

            let cardButton = document.createElement("button");
            cardButton.className = "btn-fill-animation mt-4 rounded";
            cardButton.textContent = action;
            cardButton.setAttribute("id", "buttonExercise-" + action + "-" + exercise.id);

            let division = document.createElement("hr");
            division.className = "bg-white mb-5";


            container.appendChild(divRow);
            container.appendChild(division);

            divRow.appendChild(card);

            card.appendChild(cardImage);
            card.appendChild(cardBody);

            cardBody.appendChild(cardTitle);
            cardBody.appendChild(cardButton);
        }
    } catch (e) {
        throw e;
    }

}

const thereAreNotExercises = () => {

    try {
        const container = document.getElementById("exercisesContainer");

        eliminatePastExercisesFromTheDom(container);

        const row = document.createElement("div");
        row.className = "row";

        const errorTextContainer = document.createElement("div");
        errorTextContainer.className = "col-6";

        const errorTitle = document.createElement("h1");
        errorTitle.textContent = "We are sorry";
        errorTitle.className = "text-white d-inline-block mx-3";

        const errorText = document.createElement("p");
        errorText.className = "text-white";
        errorText.textContent = "You do not have exercises created in this category";

        const imageErrorContainer = document.createElement("div");
        imageErrorContainer.className = "col-6";

        const imageError = document.createElement("img");
        imageError.setAttribute("src", "https://res.cloudinary.com/di5zdosfc/image/upload/v1624831901/Sad_face_Monochromatic_1_z5wzyr.png");


        row.appendChild(errorTextContainer);
        row.appendChild(imageErrorContainer);

        errorTextContainer.appendChild(errorTitle);
        errorTextContainer.appendChild(errorText);

        imageErrorContainer.appendChild(imageError);

        container.appendChild(row);
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
        await addCategoriesToCombo();

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

    } catch (e) {
        showMessage('error', 'Error!', e.errorMessage);
    }

});

document.getElementById("categoryCombo").addEventListener("change", async () => {

    try {

        currentIndex = 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);
    } catch (e) {
        showMessage('error', 'Error!', e.errorMessage);
    }

});

document.getElementById("size").addEventListener("change", async () => {

    try {

        currentIndex = 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);

    } catch (e) {
        showMessage('error', 'Error!', e.errorMessage);
    }
});

document.getElementById("btnPrevious").addEventListener("click", async () => {

    try {

        currentIndex -= 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);

    } catch (e) {
        showMessage('error', 'Error!', e.errorMessage);
    }
});

document.getElementById("btnNext").addEventListener("click", async () => {

    try {
        currentIndex += 1;

        document.getElementById("index").textContent = currentIndex;

        const exercises = await getExercisesByCategoryAsync(document.getElementById("categoryCombo").value, document.getElementById("index").textContent, document.getElementById("size").value);

        (exercises === null || exercises === undefined) ? thereAreNotExercises() : addTheExercisesToTheDom(exercises);

        indexesCount = await getIndexes(document.getElementById("categoryCombo").value, document.getElementById("size").value);

        enableDisablePrivousNextBtns();

        scrollTo(0, 1000);
    } catch (e) {
        showMessage('error', 'Error!', e.errorMessage);
    }

    
});
 
document.addEventListener("click", async (e) => {

    let id = e.target.id;

    let [elementName, action, exerciseId] = id.split('-');

    if (elementName !== 'buttonExercise') return;

    (action === "Add") ? await AddExercise(exerciseId) : await RemoveExercise(exerciseId);
 

});

const AddExercise = async (exerciseId) => {

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
                    ExerciseId: parseInt(exerciseId),
                    Days: days,
                    Sets: parseInt(sets)
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
    }).then((result) => {

        (result.value.succeeded) ? showMessage('success', 'Exercises added!', 'The exercise has been added to your routine successfully!') : showMessage('error', 'Error!', `The exercise could not added due to ${result.value.title}`);

    })


}

const RemoveExercise = async (exerciseId) => {
    Swal.fire({
        title: 'Are you sure?',
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

            const response = await fetch(`https://localhost:9000/v1/Routine/{exerciseId}`, settings);

            const content = await response.json();

            (content.succeeded) ? showMessage('success', 'Exercise removed!', 'The exercise has been removed from routine successfully!') : showMessage('error', 'Error!', `The exercise could not be removed from routine due to ${content.title}`);

        }
    })
}