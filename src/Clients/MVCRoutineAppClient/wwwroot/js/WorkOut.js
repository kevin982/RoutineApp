
const getExerciseToDo = async () => {

    try {

        let response = await fetch("https://localhost:9000/v1/Routine/ExerciseToDo");

        return await response.json();

    } catch (e) {
        throw e;
    }
    
}

const showExerciseToDo = (exercise) => {
    try {

        if (!exercise.succeeded) {
            Swal.fire({ background: 'black', position: 'top-center',icon: `error`,title: `Error!`,text: `${exercise.title}`,});
            return;
        }

        const exerciseContainer = document.getElementById("exerciseToDo");

        if (exercise.content === null) {

            Swal.fire({ background: 'black', position: 'top-center', icon: `success`, title: `Done`, text: `You are done for today!`, });

            const message = document.createElement("h1");
            message.className = "text-white text-center";
            message.textContent = "You are done for today!";
            exerciseContainer.appendChild(message);
            return;
        }

        document.getElementById("ExerciseId").value = exercise.content.id;

        let card = document.createElement("div");
        card.className = "card col-8 mb-5 bg-dark justify-content-center";
        card.setAttribute("id", "exerciseCard");

        let cardImage = document.createElement("img");
        cardImage.className = "card-img-top";
        cardImage.setAttribute("src", exercise.content.imageUrl);

        let cardBody = document.createElement("div");
        cardBody.className = "card-body gradient-background";

        let setsLeft = document.createElement("h2");
        setsLeft.className = "card-title text-white mt-3";
        setsLeft.textContent = `Sets left: ${exercise.content.setsLeft}`;

        let exerciseName = document.createElement("h5");
        exerciseName.className = "card-title text-white mt-3";
        exerciseName.textContent = exercise.content.name;

        exerciseContainer.appendChild(card);

        card.appendChild(cardImage);
        card.appendChild(cardBody);

        cardBody.appendChild(setsLeft);
        cardBody.appendChild(exerciseName);
        cardBody.appendChild(cardButton);

        return;


    } catch (e) {
        throw e;
    }
}

const removeExerciseDoneForm = () => {
    const result = document.getElementById("exerciseCard");

    if (result !== null) return;

    const form = document.getElementById("exerciseDoneForm");

    while (form.firstChild) {
        container.removeChild(container.firstChild);
    }
}

const validateData = () => {
    let poundsLifted = document.getElementById("PoundsLifted").value;
    let repetitions = document.getElementById("Repetitions").value;
    let exerciseId = document.getElementById("ExerciseId").value;

    if (poundsLifted == 0 || poundsLifted == "") return false;
    if (repetitions == 0 || repetitions == "") return false;
    if (exerciseId == "") return false;

    return true;
}

const sendSetDone = async () => {

    try {

        let poundsLifted = document.getElementById("PoundsLifted").value;
        let repetitions = document.getElementById("Repetitions").value;
        let exerciseId = document.getElementById("ExerciseId").value;

        const data = {
            exerciseId: exerciseId,
            poundsLifted: poundsLifted,
            repetitions: repetitions
        }

        const settings = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        };

        const response = await fetch("https://localhost:9000/v1/Routine/ExerciseDone", settings);

        return await response.json();

    } catch (e) {
        throw e;
    }

}

window.addEventListener("load", async () => {

    console.log("hola mundo!");

    try {

        let exerciseToDo = await getExerciseToDo();

        showExerciseToDo(exerciseToDo);

        removeExerciseDoneForm();

    } catch (e) {

    }
        
});

document.getElementById("btnSetDone").addEventListener("click", async () => {

    try {

        let validData = validateData();

        if (validData === false) throw new Error("The pounds lifted and repetitions must not be emtpy!");

        let response = await sendSetDone();
            
        if (!response.succeeded) throw new Error(response.title);
 
        location.reload();


    } catch (e) {
        Swal.fire({ position: 'top-center', icon: 'error', title: 'Error', text: `Could not register set done due to ${e.message}`, });
    }

});