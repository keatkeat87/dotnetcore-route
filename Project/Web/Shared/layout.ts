import { setupPixel } from '../../src/module/facebookPixel';
import { setupGA } from '../../src/module/googleAnalytics';
import { setupLiveChat } from '../../src/module/zopimLiveChat';
import { setupFacebookPage } from '../../src/module/facebookPage';
import { slideToggle, scrollToTop } from '../../src/module/dom';
import { setupExtension } from '../../src/module/extensions';
import {appConfig} from '../../src/appConfig';

export function setupLayout() {
    setupFacebookPage(appConfig.facebookAppId);
    setupPixel(appConfig.facebookPixelId);
    setupGA(appConfig.googleAnalyticsId,{
        Id: appConfig.GoogleAdsId,
        convertor:{
            'contactLink': 'M-PpCK7h0n4QrPTKgwM',
            'whatsApp': 'Ouf6CPWL0H8QrPTKgwM',
            'liveChat': '9E3SCL7j3X8QrPTKgwM',
            'emailLink': 'scsECIzdwYYBEKz0yoMD',
            'bookingStepTwoForm': 'bbTICPPfp4ABEKz0yoMD',
            'contactForm': 'bbTICPPfp4ABEKz0yoMD'
        }
    });
    setupLiveChat(appConfig.zopimLiveChatId);
    setupExtension();

    const mobileNavElem = document.getElementById('mobileNav');
    document.getElementById('pcNav').childNodes.forEach((node) => {
        const clone = node.cloneNode(true);
        mobileNavElem.appendChild(clone);
    });

    document.querySelectorAll('.sideToggle').forEach((elem) => {
        elem.addEventListener('click', () => {
            slideToggle(document.getElementById('fixSideNav'));
        });
    });

    document.getElementById('scrollToTop').addEventListener('click', () => {
        scrollToTop();
    });

    window.addEventListener('scroll', () => {
        const scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
        const scrollTopElem = document.getElementById('scrollToTop');
        if (scrollTop < 100) {
            scrollTopElem.classList.remove('show');
        }
        else {
            scrollTopElem.classList.add('show');
        }
    });
}






