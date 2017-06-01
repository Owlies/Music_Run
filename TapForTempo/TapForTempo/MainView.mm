//
//  MainView.m
//  TapForTempo
//
//  Created by Yang, Huayu on 4/20/17.
//  Copyright © 2017 Yang, Huayu. All rights reserved.
//

#import "MainView.h"

@interface MainView ()

@property (nonatomic) NSMutableArray* array;
@property (nonatomic) NSDate* startTime;

@end
@implementation MainView

- (void)drawRect:(NSRect)dirtyRect {
    [super drawRect:dirtyRect];
    
    // Drawing code here.
}

- (IBAction)StartRecord:(id)sender {
    _array = [NSMutableArray array];
    _startTime = [NSDate date];
}

- (void)keyDown:(NSEvent *)event {
    NSString* str = [event characters];

    NSTimeInterval now = [[NSDate date] timeIntervalSinceDate:_startTime];
//    NSDateFormatter *outputFormatter = [[NSDateFormatter alloc] init];
//    [outputFormatter setDateFormat:@"HH:mm:ss.SSS"];
//    NSString *curTime = [outputFormatter stringFromDate:now];
    
    NSString* record = [NSString stringWithFormat:@"%f: %@", now, str];
    NSLog(@"%f", now);
    [_array addObject:record];
}

- (BOOL)acceptsFirstResponder {
    return YES;
}

- (IBAction)StopRecord:(id)sender {
    NSString* contents = [NSString string];
    for(NSString* str in _array) {
        NSLog(@"%@", str);
        contents = [contents stringByAppendingString:[NSString stringWithFormat:@"%@\n", str]];
    }
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains
    (NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    
    //make a file name to write the data to using the documents directory:
    NSString *fileName = [NSString stringWithFormat:@"%@/TempoOutput.txt",
                          documentsDirectory];
    
    [[NSFileManager defaultManager] createFileAtPath:fileName contents:nil attributes:nil];
    
    [contents writeToFile:fileName atomically:NO encoding:NSStringEncodingConversionAllowLossy error:nil];
}

@end
