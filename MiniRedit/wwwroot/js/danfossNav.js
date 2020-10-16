(function ($) {

    //SITE MENU
    $(function () {

        var indicator = document.createElement('div');
        indicator.className = 'state-indicator';
        document.body.appendChild(indicator);

        function GetScreenState() {
            // var stateId = parseInt(window.getComputedStyle(indicator).getPropertyValue('z-index'), 10);
            //fix for ie8 using jquery
            var stateId = parseInt($(indicator).css('z-index'), 10);
            switch (stateId) {
                case 0:
                    return "default";
                case 1:
                    return "handhelds-tablets";
                case 2:
                    return "handhelds-tablets";
                case 3:
                    return "large-screens";
            }
        }

        var $el = $('.mega-menu');
        var menuEls = {};

        function HideSubnav() {
            $subnav.animate({
                opacity: 0,
                marginTop: '-4px'
            },
                100, function () {
                    $(this).removeClass('show');
                });
        }

        function ShowSubnav() {
            $subnav.finish().addClass('show').animate({
                opacity: 1,
                marginTop: '0px'
            },
                200, function () {
                    //do nothing
                });
        }


        //DESKTOP
        var $subnav = $el.find('.sub-nav');

        $el.find('ul>li').mouseover(function (e) {
            if (GetScreenState() !== 'handhelds-tablets') {

                if ($(e.currentTarget).hasClass('open')) {
                    return;
                }
                $el.find('ul>li.open').removeClass('open');

                //$subnav.removeClass('show');
                if ($(e.currentTarget).children('ul').length === 0) {
                    HideSubnav();
                }

                if ($(e.currentTarget).has("ul").length > 0) {
                    $(e.currentTarget).addClass('open');
                    //Change title
                    $subnav.find('h2').text($(e.currentTarget).find('a').first().text());

                    //Insert menu items
                    if (!menuEls[$(e.currentTarget).index()]) {
                        //Cache that to not have more page reflows
                        menuEls[$(e.currentTarget).index()] = '<ul>' + ($(e.currentTarget).find('ul').html() || '') + '</ul>';
                    }
                    $subnav.find('.sub-nav-items').html(menuEls[$(e.currentTarget).index()]);
                    $subnav.find('.sub-nav-items ul').addClass('clear-boxes-6');

                    //$subnav.addClass('show');
                    $subnav.find('.sub-nav-items ul .level-link').remove();
                    ShowSubnav();
                }
            }
        });




        $el.mouseleave(function (e) {
            $el.find('ul>li').removeClass('open');
            //$subnav.removeClass('show');
            HideSubnav();
        });

        //MOBILE
        $el.find('.menu-btn').click(function (e) {
            e.preventDefault();
            $el.find('.mobile-btn').removeClass('open');
            $el.find('.search').removeClass('open');
            $el.find('.main-nav').toggleClass('open');
            $el.find('.nav-top').toggleClass('open');
            $('body').removeClass('corporate-global-visible');

            if ($el.find('.nav-top').hasClass('open')) {
                $(e.currentTarget).addClass('open');
            }
        });

        $el.find('a').click(function (e) {
            if (GetScreenState() === 'handhelds-tablets') {
                if ($(e.currentTarget).parent().find('ul li').length > 0) {
                    e.preventDefault();
                    $(e.currentTarget).parent().toggleClass('open');
                }
            }
        });

        $el.find('.search-btn').click(function (e) {
            e.preventDefault();
            $el.find('.mobile-btn').removeClass('open');
            $el.find('.main-nav').removeClass('open');
            $el.find('.nav-top').removeClass('open');
            $('body').removeClass('corporate-global-visible');

            if ($el.find('.search').hasClass('open')) {
                $el.find('.search').removeClass('open');
            } else {
                $(e.currentTarget).addClass('open');
                setTimeout(function () {
                    $('.search-input').focus();
                }, 0);
                $el.find('.search').addClass('open');
            }

        });

        /* ** SEARCH ** */
        // expands on focus on search icon button or field, blurs on the same
        $(".container.search .inner").each(function () {
            var $el = $(this);

            $el.on("focus", "input", function () {
                $el.addClass("active");
                return false;
            });
            $el.on("blur", "input", function () {
                setTimeout(function () {
                    $el.removeClass('active');
                }, 120);
            });
        });

        // prevents empty searches that breaks functionality
        $(".container.search .inner input[type='submit']").click(function (e) {
            if ($(this).siblings(".search-input").val() === "") {
                if (window.event) {
                    window.event.returnValue = false;
                }
                e.preventDefault();
            }
        });

        // lets the user press the search icon as an alternative to pressing the enter key
        // blurs the field after press to make sure it collapses
        $(".container.search .icon-search").click(function (e) {
            $(this).siblings('input[type="submit"]').click();
            $(this).siblings('input').blur();
        });
        /* ** SEARCH END ** */

        var SCROLL_TRIGGER_TOP = 80;
        var isPopupWindow = $('.top-slim').length ? true : false;

        //SCROLLSPRY
        var $body = $('body');
        if (!isPopupWindow) {
            if ($(window).scrollTop() > SCROLL_TRIGGER_TOP) {
                $('.mega-menu, .corporate-global-container').addClass('fixed');
            }
            $(window).on('scroll', function () {
                var scrollTop = $(window).scrollTop();
                if (scrollTop > SCROLL_TRIGGER_TOP) {
                    $('.mega-menu').removeClass('expanded');

                    if (!$('.mega-menu').hasClass('fixed') && !$body.hasClass('corporate-global-visible')) {
                        $('.mega-menu, .corporate-global-container').addClass('fixed');
                    }
                } else {
                    if ($('.mega-menu').hasClass('fixed')) {
                        // $body.removeClass('mega-menu-fixed');
                        $('.mega-menu, .corporate-global-container').removeClass('fixed');
                        // $('.mega-menu, .corporate-global-container').off('mouseenter');
                    }
                }
            });

            //--Mouse over handlers
            $('.mega-menu, .corporate-global-container').on('mouseenter', function () {
                if (GetScreenState() === 'handhelds-tablets') {
                    return false;
                }
                if (!$('.mega-menu').hasClass('expanded')) {
                    $('.mega-menu').addClass('expanded');
                }
                $('.mega-menu, .corporate-global-container').on('mouseleave', function () {
                    if ($('.mega-menu').hasClass('expanded')) {
                        $('.mega-menu').removeClass('expanded');
                    }
                });
            });
        }

    });
})(jQuery);