function scrollIntoView(id) {
    var element = document.getElementById(id);
    element.scrollIntoView({block: "center", inline: "nearest"});
}

function createChips(txtToCreate) {
    var input = document.querySelector(".chip-input");
    var chips = document.querySelector(".chips");
    chips.appendChild(function () {
        var _chip = document.createElement('div');

        _chip.classList.add('chip');
        _chip.addEventListener('click', chipClickHandler);

        _chip.append(
            (function () {
                var _chip_text = document.createElement('span');
                _chip_text.classList.add('chip--text');
                _chip_text.innerHTML = txtToCreate;
                // var clearbtn = document.getElementsByClassName('autocomplete-clear-button');
                // if (clearbtn.length > 0) {
                //     clearbtn[0].click()
                // }
                return _chip_text;
            })(),
            (function () {
                var _chip_button = document.createElement('span');
                _chip_button.classList.add('chip--button');
                _chip_button.innerHTML = 'x';

                return _chip_button;
            })()
        );

        return _chip;
    }());
    //input.value = '';
}
function bindChipControls() {
    var input = document.querySelector(".chip-input");
    var chips = document.querySelector(".chips");

    document.querySelector(".form-field")
        .addEventListener('click', () => {
            input.style.display = 'block';
            input.focus();
        });

    input.addEventListener('blur', () => {
        input.style.display = 'none';
    });

    input.addEventListener('keypress', function (event) {
        if (event.which === 13) {
            chips.appendChild(function () {
                var _chip = document.createElement('div');

                _chip.classList.add('chip');
                _chip.addEventListener('click', chipClickHandler);

                _chip.append(
                    (function () {
                        var _chip_text = document.createElement('span');
                        _chip_text.classList.add('chip--text');
                        _chip_text.innerHTML = input.value;
                        // var clearbtn = document.getElementsByClassName('autocomplete-clear-button');
                        // if (clearbtn.length > 0) {
                        //     clearbtn[0].click()
                        // }
                        return _chip_text;
                    })(),
                    (function () {
                        var _chip_button = document.createElement('span');
                        _chip_button.classList.add('chip--button');
                        _chip_button.innerHTML = 'x';

                        return _chip_button;
                    })()
                );

                return _chip;
            }());
            input.value = '';
        }
    });
}
function chipClickHandler(event) {
    chips.removeChild(event.currentTarget);
}