const GetExercises = token =>
{
    console.log("Hola mundo, el token es: ", token);
}






const getExercisesByCategoryAsync = async (categoryId) => {

    const resp = await fetch(`https://localhost:5001/Exercise/GetUserExercisesByCategory/${categoryId}`);

    //const resp = await fetch("https://localhost:5001/Exercise/GetName");

    const respJson = await resp.json();

    return respJson;
}


const eliminatePastExercisesFromTheDom = (container) => {

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }

}




const addTheExercisesToTheDom = (exercises) => {



    const container = document.getElementById("exercisesContainer");

    eliminatePastExercisesFromTheDom(container);

    for (let i = 0; i < exercises.length; i++) {

        let exercise = exercises[i];
        let img = exercise.Image;

        console.log(exercise);

        let divRow = document.createElement("div");
        divRow.className = "row justify-content-center";

        let card = document.createElement("div");
        card.className = "card col-8 mb-5 bg-dark";

        let cardImage = document.createElement("img");
        cardImage.className = "card-img-top";
        cardImage.setAttribute("src", img);

        let cardBody = document.createElement("div");
        cardBody.className = "card-body gradient-background";

        let cardTitle = document.createElement("h2");
        cardTitle.className = "card-title text-white mt-3";
        cardTitle.textContent = exercise.ExerciseName;

        let cardText = document.createElement("p");
        cardText.className = "card-text text-white";
        cardText.textContent = `This exercise belongs to the ${exercise.Category} category`;

        let action = (exercise.IsInTheRoutine) ? "Remove" : "Add";

        let cardButton = document.createElement("button");
        cardButton.className = "btn-fill-animation mt-4 rounded";
        cardButton.textContent = action;
        cardButton.setAttribute("id", "buttonExercise-" + action + "-" + exercise.ExerciseId);

        let division = document.createElement("hr");
        division.className = "bg-white mb-5";


        container.appendChild(divRow);
        container.appendChild(division);

        divRow.appendChild(card);

        card.appendChild(cardImage);
        card.appendChild(cardBody);

        cardBody.appendChild(cardTitle);
        cardBody.appendChild(cardText);
        cardBody.appendChild(cardButton);



    }


}

const informThereIsNotExercises = () => {
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
}


const categoryCombo = document.getElementById("categoryCombo");

window.addEventListener("load", async () => {
    const exercises = await getExercisesByCategoryAsync(categoryCombo.value);

    console.log(exercises);

    addTheExercisesToTheDom(exercises);
});


categoryCombo.addEventListener("change", async () => {

    const exercises = await getExercisesByCategoryAsync(categoryCombo.value);

    console.log(exercises);

    if (exercises.length === 0) {
        informThereIsNotExercises();
    } else {
        addTheExercisesToTheDom(exercises);
    }


});

document.addEventListener("click", async (e) => {

    let id = e.target.id;

    let [elementName, action, exerciseId] = id.split('-');

    if (elementName !== 'buttonExercise') return;

    if (action === "Add") {
        await AddExercise(exerciseId);
    } else if (action === "Remove") {
        await RemoveExercise(exerciseId);
    }


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

            if (!sets || !days) {
                Swal.showValidationMessage(`Please enter sets and days`)
            }

            for (var i = 0; i < days.length; i++) days[i] = parseInt(days[i]);

            const addExerciseModel = {
                ExerciseId: parseInt(exerciseId),
                Days: days,
                Sets: parseInt(sets)
            };

            const settings = {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(addExerciseModel)
            };


            const response = await fetch("https://localhost:44380/WeatherForecast/AddExercise", settings);

            //const response = await fetch("https://localhost:44380/WeatherForecast/Get");

            //const response = await fetch("https://localhost:5001/Exercise/AddName");

            const json = await response.json();

            console.log(response.body);


            return { sets: sets, days: days }
        }
    }).then((result) => {
        Swal.fire(`
    Sets: ${result.value.sets}
    Days: ${result.value.days}
  `.trim())
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
    }).then((result) => {
        if (result.isConfirmed) {



            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
}