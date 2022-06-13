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
    let tagCount = 1;
    let tagList = document.getElementById("TagValues");
    if (!tagList) return false;
    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>CHOOSE A TAG BEFORE DELETING</span>"
        });
        return true;
    }
    while (tagCount > 0) {               
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagValues option").prop("selected", "selected");
})

//Look for the tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //Load up or Replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}



//The Search funtion will detect either an empty or a duplicate Tag
//and return an error string if an error is detected


function search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }
    
    var tagsEl = document.getElementById('TagValues');
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `The Tag #${str} was detected as a duplicate and not permitted`;
            }
        }
        
    }

}

