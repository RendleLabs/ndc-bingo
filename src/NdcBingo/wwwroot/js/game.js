$(function () {
    const selectedTextElement = $('#selected-text');
    const selectedDescriptionElement = $('#selected-description');
    const claimButton = $('#claim-button');
    claimButton.hide();
    var selectedCard;
    
    $('.bingo-square').click(function (e) {
        const $this = $(this);
        if (selectedCard) {
            selectedCard.toggleClass('active');
            if (selectedCard.attr('data-square-id') === $this.attr('data-square-id')) {
                selectedTextElement.text('');
                selectedDescriptionElement.text('');
                claimButton.hide();
                selectedCard = null;
                return;
            }
        }
        selectedCard = $this;
        selectedCard.toggleClass('active');
        selectedTextElement.text($this.attr('data-square-text'));
        selectedDescriptionElement.text($this.attr('data-square-description'));
        const link = $this.attr('data-claim-link');
        if (!!link) {
            claimButton.attr('href', link);
            claimButton.show();
        } else {
            claimButton.attr('href', '');
            claimButton.hide();
        }
    });
});