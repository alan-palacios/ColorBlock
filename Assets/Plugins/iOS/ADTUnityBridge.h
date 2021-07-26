//
//  ADTUnityBridge.h
//  Unity-iPhone
//
//  Created by ylm on 2019/7/31.
//

#import <Foundation/Foundation.h>
#import "ADTClass.h"

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSInteger, ADTForUnityAdState) {
    ADTForUnityAdStateAvailable = 0,
    ADTForUnityAdStateOpen = 1,
    ADTForUnityAdStateShow = 2,
    ADTForUnityAdStateClick = 3,
    ADTForUnityAdStateClose = 4,
    ADTForUnityAdStateEnd = 5,
    ADTForUnityAdStateShowFail = 6,
    ADTForUnityAdStateRewarded = 7,
};

typedef void (^interstitialCallbackBlock)(ADTForUnityAdState state, NSInteger code, NSString *extraData);
typedef void (^videoCallbackBlock)(ADTForUnityAdState state, NSInteger code, NSString *extraData);

@interface ADTUnityBridge : NSObject<AdTimingInterstitialDelegate,AdTimingRewardedVideoDelegate,AdTimingBannerDelegate>
@property (nonatomic, strong) NSMutableDictionary* bannerAdMap;
@property (nonatomic, strong) NSMutableDictionary* bannerPostionMap;
@property (nonatomic, strong) interstitialCallbackBlock interstitialBlock;
@property (nonatomic, strong) videoCallbackBlock videoBlock;
+ (instancetype)sharedInstance;
//init
- (void)initWithAppKey:(NSString*)appKey;

//interstitial
- (BOOL)interstitialIsReady;

- (void)showInterstitial;

- (void)showInterstitialWithScene:(NSString *)scene;

//video
- (BOOL)videoIsReady;

- (void)showVideo;

- (void)showVideoWithScene:(NSString *)scene;

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams;


//Banner
- (void)loadBanner:(NSInteger)bannerType position:(NSInteger)position placement:(NSString *)placementID;

- (void)destroyBanner:(NSString*)placementID;

- (void)displayBanner:(NSString*)placementID;

- (void)hideBanner:(NSString*)placementID;

@end


NS_ASSUME_NONNULL_END
