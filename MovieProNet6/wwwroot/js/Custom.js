let index = 0;

function AddMovie() {
    //Get a reference to the TagEntry input element
    var movieEntry = document.getElementById("MovieTMDBValues");

    //Lets use the new search funstion to help detect an error state
    let searchResult = search(movieEntry.value);
    if (searchResult != null) {
        alert("You select nothing")
    }
    else {
        //Create a new Select Option
        let newOption = new Option(movieEntry.value, movieEntry.value);
        document.getElementById("MovieLibraryValues").options[index++] = newOption;
    }       
    movieEntry.value = "";
    return true;
}

function DeleteTag() {
    let movieCount = 1;
    let movieList = document.getElementById("MovieLibraryValues");
    if (!movieList) return false;
    if (movieList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>CHOOSE A TAG BEFORE DELETING</span>"
        });
        return true;
    }
    while (movieCount > 0) {               
        if (movieList.selectedIndex >= 0) {
            movieList.options[movie.selectedIndex] = null;
            --movieCount;
        }
        else
            movieCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#MovieLibraryValues option").prop("selected", "selected");
})

//Look for the tagValues variable to see if it has data
if (movieLibraryValues != '') {
    let movieLibraryArray = movieLibraryValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //Load up or Replace the options that we have
        ReplaceTag(movieLibraryArray[loop], loop);
        index++;
    }
}

function ReplaceMovie(movie, index) {
    let newOption = new Option(movie, movie);
    document.getElementById("MovieList").options[index] = newOption;
}



//The Search funtion will detect either an empty or a duplicate Tag
//and return an error string if an error is detected


function search(str) {
    if (str == "") {
        return 'You must choose a movie';
    }
    
    var moviesEl = document.getElementById('MovieLibraryValues');
    if (moviesEl) {
        let options = moviesEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `The Movie #${str} is already in the Library`;
            }
        }
        
    }

}

