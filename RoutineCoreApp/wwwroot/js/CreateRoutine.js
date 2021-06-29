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

        let cardButton = document.createElement("a");
        cardButton.className = "btn-fill-animation mt-4 rounded";
        cardButton.textContent = action;
        cardButton.setAttribute("href", `https//localhost:5001/Exercise/${action}/${exercise.ExerciseId}`);
        cardButton.setAttribute("role", "button");

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


